#lang racket
(require racket/trace
         racket/system
         racket/file
         2htdp/image)
;Ejercicio de alfabetos
;1.-Un alfabeto de 5 caracteres
(define S1 '(a b c d e))
;2.-Un alfabeto de 4 caracteres
(define S2 '(w x y z))
;3.-Un alfabeto de 3 caracteres
(define S3 '(amarillo rojo azul))
;4.-Un alfabeto de 2 caracteres
(define S4 '(0 1))
;5.-Un alfabeto de 1 caracteres
(define S5 '(x))

;======================================================================
;Fundamentos de M.D.
;======================================================================
;Redefine empty a vacio
(define vacio? empty?)

;Determina si un elemento e pertenece al conjunto C
(define en?
  (λ(k S)
    (cond((vacio? S) #f)
         ((equal? k (car S))#t)
         (else (en? k (cdr S)))                     
    )))

;cuantificador universal
;(para-todo P A)-->booleano
;P : predicado
;A : conjunto
(define para-todo
 (λ(P A)
   (cond((vacio? A)#t)
       ((P(car A))(para-todo P(cdr A)))
       (else #f))))

(define para-Todo andmap)

(define existe-Un ormap)

;cardinalidad de un conjunto
(define card length)

;(to-conjunto lst) -->conjunto
;lst : lista
(define to-conjunto
  (λ(lista[res '()])
    (cond((empty? lista) res)
        ((en? (car lista)(cdr lista))(to-conjunto(cdr lista)res)) ; si se repite, no lo agrego
        (else(to-conjunto(cdr lista)(cons(car lista)res))))))


;agregar un elemento a un conjunto
;(agregar e C)--conjunto
;e : cualquiera
;C : conjunto
(define agregar
  (λ(e C)
    (if(en? e C)
       C
       (cons e C))))

;union de dos conjuntos
;(union A B)-->conjunto
;A : conjunto
;B : conjunto
(define union
  (λ(A B)
       (if(empty? A)B
         (union(cdr A)(agregar(car A)B)))))

;union generalizada
;(union* LC)-->conjunto
;LC : lista de conjuntos
(define union*
  (λ  LC    
    (define union*aux
      (λ (lc [res'()])
        (if(vacio? lc)
           res
           (union*aux(cdr lc)(union(car lc)res)))))
(if(vacio? LC)
   '()
   (union*aux LC))))

;crea un conjunto que es la interseccion de 2 conjuntos dados
;(interseccion-aux A B [C '()])-->conjunto
;A : conjunto
;B : conjunto
;C : conjunto ,no usar como entrada en 1ra llamada
(define interseccion-aux
  (λ(A B [C '()])
    (cond((vacio? B)'())
         ((vacio? A) C)
         ((en? (car A)B)(interseccion-aux (cdr A)B (cons (car A)C)))
          (else(interseccion-aux (cdr A)B C)))))

;produce la interseccion de dos conjuntos
;(interseccion A B) -->conjunto
;A : conjunto
;B : conjunto
(define interseccion
  (λ(A B)
    (interseccion-aux A B '())))

;(inter* lst-conj) -->conjunto
;lst-conj lista de conjuntos
(define inter*
  (λ LC
    (define inter*aux
      (λ(lc [res '()])
        (if(vacio? lc)
           res
           (inter*aux (cdr lc)(interseccion res (car lc))))))
    (cond((vacio? LC) '())
         ((vacio? (cdr LC)) (car LC))
         (else (inter*aux(cdr LC)(car LC ))))))

;la negacion de una proposicion
;(neg p) --> booleano?
;p : booleano?
(define neg ;definiendo negacion
  (λ(p);definir procedimiento que recibe una propocision
    (if p #f #t)));regresa el valor contrario de la preposicion

;la conjuncion de dos proposiciones
;(y p q) --> booleano?
;p : booleano?
;q : booleano?
(define y
  (λ(p q)
    (if p q #f)))

;la disyuncion de dos proposiciones
;(o p q) --> booleano?
;p : booleano?
;q : booleano?
(define o
  (λ(p q)
    (if p #t q)))

;la disyuncion exclusiva
;(ox p q) --> booleano?
;p : booleano?
;q : booleano?
(define ox
  (λ(p q)
    (if p (neg q) q)))

;la implicacion
;(-> p q) --> booleano?
;p : booleano?
;q : booleano?
(define ->
  (λ (p q)
  (if p q #t)))


;inicia con el caso base del vacio
;a la iteracion que sigue le copio el anterior y le agrego una copia
;pero a cada copia se le agrega el elemento extra
;conjunto potencia
;(conj-pot C) -->lista de conjuntos
;C : conjunto
(define conj-pot
  (λ (C [base '(())]) 
    (define addcada
      (λ(e LC)
        (map (λ(C) (agregar e C)) LC)))
    (if (vacio? C)
        base
        (conj-pot(cdr C) (append base (addcada(car C) base))))))

;determina el dominio
(define Dominio
  (λ (R)
    (map(λ (e) (drop-right e 1))R)))

;calcula la imagen de un elemento x en la relacion R
; (Im x R) --> conjunto
; R : Lista/de listas
(define Imagen
  (λ (D R)
    (map( λ (k) (last k))
        (filter (λ (i)(equal? (drop-right i 1) D))R))))

;determina si la relacion es una funcion
(define esFun?
  (λ (R)
    (para-Todo (λ (im) (= (card im)1))
              (map (λ (x) (Imagen x R)) (Dominio R)))))


;======================================================================
;Simbolos, alfabetos, palabras y lenguajes
;======================================================================
;Determina si una letra k pertenece a un alfabeto S
(define enA?
  (λ(k S)
    (en? k S)))

;define la palabra vacia
(define pVacia '())

;Crea una palabra de cualquier lista de simbolos
(define pal list)

;Crea una palabra unitaria dando una letra k y un alfabeto S
(define p-unit
  (λ(k [S '()])
    (cond((enA? k S)(pal k))
         ((equal? S '())(pal k))
         (else #f))))

;======================================================================
;Tarea 1.01 Palabra w construida con el alfabeto S
;w : palabra
;S : alfabeto
;k : letra
;en las palabras si aceptamos repetidos
;en el alfabeto no
;pconS w S) --> booleano
(define pconS
  (λ(w S)
    (para-Todo(λ(k)(enA? k S))w) 
      ))

;decimos que una palabra w esta construida con el albateo s
;si paratoda s en w entonces se cumple que w en S

;calcula la longitus de una palabra w
;w : palabra
;(plong w) --> numero-entero-no-neg
(define plong
  (λ(w)
    (length w)))

;escribe una letra k por la izquierda de una palabra w
;k : letra
;w : palabra
;(w-izq k w) --> w
(define w-izq
  (λ(k w)
    (cons k w)))

;concanetacion de 2 palabras
;w...+ : palabras(al menos una)
;        ...+ significa "al menos una"
;        ...  significa "las que sean, incluso ninguna"
;(pcatp w...+) --> palabra
(define pcatp append)

;escribe una letra k por la derecha de una palabra w
;k : letra
;w : palabra
;(w-der k w) --> w
(define w-der
  (λ(k w)    
    (pcatp w(p-unit k))))

;obtiene una letra aleatoria de un alfabeto
;S : alfabeto
(define lrandom
  (λ(S)
    (car(list-tail S (random(plong S))))))

;TAREA 1-02 palabra aleatoria
;genera una palabra aleatoria de una longitud dada y con un alfabeto dado
;n : numero entero no negativo
;S : alfabeto
;(wrandom n S)--> palabra
(define wrandom
  (λ(x S [m '()])
    (if(zero? x) m
    (wrandom (- x 1) S (append m (list(lrandom S)))))))

(define prnd
  (λ(n S)
    (let ((l(card S))) ;l == card S
      (map (λ(i)(list-ref S(random l)))
           (build-list n values))))) 

;longitud =n y  todas las letras pertenecen a sigma
;sigma n = {w : |w| }=n y pconS(w)}

;===================================================================
;varias palabras
;generaliza el concepto de w-der
;k : letra
;W : coleccion de palabras
;(w-der*)-->conjunto-palabras(incluso vacia)
(define w-der*
  (λ(k W);W es una coleccion de palabras
    (map(λ(w)(w-der k w))W)))

;Tarea 1.03
;calcula la potencia n de un alfabeto S
;S : alfabeto
;n : numero-entero-no-negativo
;(Spot n S)--> conjunto de palabras [Lenguaje]
;todas las palabras del alfabeto con el numero de letras dado
(define Spot
  (λ(S n [res '(())])
    (if(= n 0)
       res
       (Spot S (- n 1)
       (append*(map (λ(s) (w-der* s res));
             ;(map(λ(w)(w-der s w))
              ;     res))
             S))));para cada palabra w que esta en res
))
;(map(λ(a)(map(λ(b)(list a b))'(1 2 3)))
;apply j,k  usa j como operador dentro de la lista k
;append*
        
;(map(λ(i)(+ 1 i))'(1 2 3))

;stephen kleene

;Σ* = {w : pconS (w,S) y |w| < inf}
         ;la palabra esta  ;la longitud de la palabra es finita
         ;hecha con simbolos en S

;Σ* = U(i=0, k<inf) Σi
; = Σ+ U Σ U...U Σk
;=U(i=0,k) Σi

(define SGen
         (λ(w)
           (remove-duplicates w)))
;==================================================================
;Tarea 1.04
;w : lista-de-palabras
;(wGen* w)-->alfabeto
;devuelve el alfabeto que genera la palabra
;1ro obtener el alfabeto de una palabra
;2do unir todos los alfabetos con la union*
(define wGen* union*)

;Tarea 1.05========================================================
;aux-alfabeto
(define alfa*
  (λ (S [n 10][res '()])
  (if(empty? S)
     (list res)
     (alfabeto* S n res))))

;alfabeto*
(define alfabeto*
  (λ(S n [res '()])
    (if(= n 0)
       res
    (list (alfabeto* S (- n 1)(append res S))res))))



;ks*
;(define ks*
;  (λ(S [k 3] [res '()])
;    (if(= k 0)
;       res
;    (alfabeto* S (- n 1)(append res S))res)))

;potencia de una palabra
;crea concatenaciones de una palabra w consigo misma n veces
;(w-potencia w n)-->palabra
;w: palabra
;n : entero no negativo
(define w-pot
  (λ(w n[res '()])
    (if (= n 0)
        res
        (w-pot w (- n 1)(append res w)))))


;Σ* todas las palabras de longitud finita generadas con simbolos de un alfabeto generador, aka el conjunto de palabras
(define pVacia?
  (λ(p)
    (empty? p)))
    ;(equal? p pVacia)))

(define p= equal?)
;  (λ(w1 w2)
 ;   (cond((y (empty? w1)(empty? w2)) #t)
  ;       ((ox (empty? w1)(empty? w2)) #f)
   ;      ((equal? (car w1)(car w2))(p= (cdr w1) (cdr w2)))
    ;     (else #f))
     ;    ))

;digamos que Alpha =<mar> y w=<martillo>
;alpha es prefijo de w por que si beta=tillo y hacemos alpha . beta
;eso es = <mar><tillo>=<martillo>=w.
;de manera similar si beta=<tillo> y w =<martillo>
;beta es sufijo de w porque si alpha =<mar> y
;alpha . beta = <mar><tillo> =<martillo> 0 w
;aqui alpha es prefijo propio
;y beta es sufijo propio

;condiciones: caso vacio
;caso 1ra letras
;TAREA 1.06 ===================================================================
;determina si una palabra <a> es prefijo de otra palabra <w>
;(pefijo? wa wb)-->booleano
;wa : palabra
;wb : palabra
(define prefijo?
  (λ (wa wb)
    (and (<= (plong wa) (plong wb)) (p= wa (take wb (plong wa))))))
;si se cumple que a <= b y
;si estas dos secciones son las mismas
;(define prefijo-x?
;  (λ (a w)
;    (if(>(plong a)(plong w))
;       #f
;       (equal? a(take w(plong a))))))

;(define prefijo-x?
;  (λ (a w)
;    (cond((pVacia? a)#t)
 ;        ((pVacia? w)#f)
 ;        ((equal?(car a)(car w))(prefijo-x? (cdr a)(cdr w)))
 ;        (else #f))))

(define prefijo
  (λ(w g [res '()])
  (if(prefijo? g w)
     res
     (prefijo (cdr w) g (w-der(car w)res)))))

                         
(define posfijo
  (λ(w g [res '()])
  (if(sufijo? g w)
     res
     (posfijo (drop-right w 1) g (w-izq(last w)res)))))
                         
;aka posfijo
;TAREA 1.07 ===================================================================
;determina si una palabra <b> es prefijo de otra palabra <w>
;(sufijo? b w)-->booleano
;b : palabra
;w : palabra
;(define sufijo?
 ; (λ (b w)
  ;  (and (<= (plong b) (plong w)) (p= b (take-right w (plong b))))))

(define sufijo?
  (λ (b w)
    (cond((pVacia? b)#t)
         ((pVacia? w)#f)
         ((equal?(last b)(last w))(sufijo? (drop-right b 1)
                                             (drop-right w 1)))
         (else #f))))


;TAREA 1.08 ==================================================================
;determina si una palabra <w1> es subpalabra de otra palabra <w2>
;(subpalabra? w1 w2)-->booleano
;w1 : palabra
;w2 : palabra
(define subpalabra-v1?
  (λ (w1 w2)
    (let* ((wj (map (λ (j) (take-right w2 j)) (build-list (+ 1 (length w2)) values)))
           (wm (map (λ (m) (take w2 m)) (build-list (+ 1 (length w2)) values)))
           (sub (map (λ (B)
                       (map (λ (A) (pcatp(pcatp A w1) B)) wm))
                     wj))
           )
      (existe-Un (λ (s) (p= s w2)) (append* sub)))))
;recorro a w2 buscando encontrar que este igual un segmento a w1
;j>k<m

;version optimisada
(define subpalabra?
  (λ(g w)
    (cond((pVacia? g)#t)
         ((pVacia? w)#f)
         ((prefijo? g w)#t)
         (else (subpalabra? g(cdr w))))))

;TAREA 1.09 ===============================================================================================
;(subpalabra g w)-->lista de palabras pre-w-pos o falso
;g : palabra
;w : palabra
(define subpalabra
  (λ(g w)
    (cond((pVacia? g)(list(take w(quotient (plong w)2))
                               '()
                               (drop w(quotient(plong w)2))))
         ((subpalabra? g w)(list (prefijo w g) g(posfijo w g)))
         (else #f))))
    


;calcula la inversa de una palabra
;(p-inversa w) -->palabra
;w : palabra
;(define p-inversa
;  (λ(w)
;    (if (pVacia? w)
;        pVacia
;        (pcatp(p-inversa(cdr w))(p-unit(car w)))
;    )))
;
;(define p-inv
;  (λ(w [res '()])
;    (if(pVacia? w)
;       res
;       (p-inv(cdr w)(pcatp (p-unit(car w)) res)))))

(define p-inversa reverse)

;tarea 1.09 
;concanetacion de dos lenguajes
;L1 : lista no vacia
;L2 : lista no vacia
;(lcatl L1 L2)-->lista
(define lcatl
  (λ (L1 L2)
    (append*
     (map(λ(x)
            (map(λ(y)
                  (append x y)) L2)
           ) L1)
     )))

;tarea 1.10
;potencia de un lenguaje
;(l-pot A n)-->lenguaje
;(l-pot '((0)(1))2)-->'((0 0)(0 1)(1 0)(1 1)
;A : lenguaje
;n : entero >= 0
(define l-pot
  (λ(A n [res '(())])
    (if (= n 0)
        res
     (l-pot A (- n 1) (lcatl A res))
    )))

;tarea 1.11
;sublenguaje
;(sub-l L1 L2) -->lenguaje
;L1 : lenguaje
;L2 : lenguaje
(define sub-l
  (λ(L1 L2)
    (andmap(λ(w) (en? w L2))L1)))

;igualdad de lenguajes
;(l=? L1 L2) -->booleano
;L1 : lenguaje
;L2 : lenguaje
(define l=?
  (λ(L1 L2)
    (and (sub-l L1 L2)(sub-l L2 L1))))


;union de lenguajes
;(union-l L1 L2)-->lenguaje
;L1 : lenguaje
;L2 : lenguaje
(define union-l union*)


;interseccion de lenguajes
;(inter-l L1 L2)-->lenguaje
;L1 : lenguaje
;L2 : lenguaje
(define inter-l inter*)

(define pez%
  (class object%
    (init largo)
    (define largo-actual largo)
    (super-new)
    (define/public (get-largo)
      largo-actual)
    (define/public(crece cantidad)
      (set! largo-actual(+ largo-actual cantidad)))
    (define/public(come otro-pez)
      (crece(send otro-pez get-largo)))))

(define(suma-1 a b)
  (+ a b))

(define suma-2
         (λ(a b)
           (+ a b)))
;================================================================================================================================
;verifica que sea una palabra de un alfabeto dado
;(palabra? w S) --> boleano?
;w: cualquiera
;S: alfaberto
(define palabra?
  (λ (w [S '()])
    (if (empty? S)
       (list? w) 
    (y (list? w) (para-todo (λ (s) (enA? s S)) w))
    )))
   
;el conjunto de todas las palabras hechas con simbolos de un alfabeto A
;(nkleene A [n] [res])->lenguaje
;A: alfabeto
;n: numero entero no negativo
;res: alfabeto='(())
(define nKleene
  (λ (A [n 10] [res '(())])
    (define nKleene-aux
      (λ (A [n 10] [res '(())])
          (if (= n 0)
              res
              (nKleene-aux A (- n 1) (union* res (l-pot A n))))))
    (if (and (para-todo (λ (s) (palabra? s)) A))
        (nKleene-aux A n res)
        (nKleene-aux (map (λ (s) (p-unit s)) A) n res))))    

;======================================================================================
;AUTOMATAS FINITOS DETERMINISTAS
;======================================================================================
;(field [E(list-ref Ldef 0)])
(define afd%
  (class object%  
  (init AF-conf)                ; initializacion de argumentos
  (define E (list-ref AF-conf 0));estado
  (define S (list-ref AF-conf 1));alfabeto
  (define e0 (list-ref AF-conf 2));estado inicial
  (define A (list-ref AF-conf 3));aceptores
  (define T (list-ref AF-conf 4));tranciciones
    (super-new)
    (define/public(get-E) E)
    (define/public(get-S) S)
    (define/public(get-e0) e0)
    (define/public(get-A) A)
    (define/public(get-T) T)
    ;transicion
    ;obtener el siguiente estado al que accede el AFD
    ;(tran e l) --> e
    ;e : estado
    ;l : letra
    ;(send atest tran 2 'A)--> 3
    ;(define/public (tran q s)
    ;  (last(car
     ;         (filter (λ (t) (equal? (list q s) (take t 2))) T)))
    ;    )
    ;(define/public (tran q s T)
     ; ((cond(empty? T)#f)
     ; (equal? (list q s)(list(caar T)(cadar T))(last T))
     ; (else(tran q s(cdr T)))))
    
   (define/public(tran e s)
  ;   (printf "~s: ~s: ~n" e s)
      (last(car(filter(λ(t);filtrame las que no cumplen con
                        (equal? (list e s)(drop-right t 1))) ;esta condicion
                     T;tomadas de este conjunto
                      ))))

    ;generalizacion de la transicion
    ;determina el estado final al que AFD accede despues de leer todas las letras de la palabra
    ;desde un estado inicial dado
    ;(tran* a1 1 '(C C C A C C) --> 1
    ;tran*
    (define/public (tran* edo palabra)
      (if (pVacia? palabra)
          edo
          (tran* (tran edo (car palabra)) (cdr palabra))
          ))
    
    ;aceptor
    ;determina si el termino es un estado aceptor
    ;e : estado
    ;A : lista de estados aceptores
    ;(aceptor e)-->boleano
    ;(send a1 aceptor 'e1) --#t    ;(send a1 aceptor 'e5)-->#f
    (define/public (aceptor e)
      (en? e A))
    
    ;aceptada
    ;pregunta si la palabra es aceptada por lenguaje del automata
    ;--si al terminar la palabra esta en un estado aceptor o no--
    ;w : palabra
    ;(aceptada? w)-->boleano
    (define/public (aceptada? w)
      (en?(tran* e0 w)A))

;===============================================================================================================================   
    ;lenguaje del afd
    ;produce el conjunto de palabras aceptadas por el automata
    ;(leng [k])-->lenguaje
    ;k : entero mayor o igual que cero
    (define/public (lenguaje [k 6])
      (let ((S* (nKleene S k)))
        (filter (λ (w) (aceptada? w)) S*)))
;===================================================================================================================================

));clase afd
(define estados '(1 2 3 4 5))
(define letras '(A B C))
(define estado-0 '1)
(define aceptores '(1))
(define transis '((1 A 2)(2 C 1)(1 B 3)(3 C 1)(2 A 4)(3 B 5)(4 A 4)(4 B 4)(5 A 5)(5 B 5)(1 C 1)(2 B 5)(3 A 4)))
;(define a1(new afd% [-E estados][-S letras][-e0 estado-0][-A aceptores][-T transis]))
;no usar a1, esta incompleto
(define ete '(1 2))
(define etl '(A B))
(define etei '1)
(define eta '(1))
(define ett '((1 A 1)(1 B 2)(2 B 1)(2 A 2)))
;(define eatest(new afd%[-E ete][-S etl][-e0 etei][-A eta][-T ett]))
(define eatest(new afd%[AF-conf(list ete etl etei eta ett)]))

(define creaT
  (λ (e la [t '()])
    (if (empty? la) t
    (creaT e (drop la 2) (agregar (cons e (take la 2)) t)))))

(define diferencia
  (λ (A B)
    (filter-not (λ (a) (en? a B)) A)))   

;calcula T con juego de string
(define 2file->afd
  (λ(nomarch)
    (let* ((P1 (file->lines nomarch #:mode 'text))
           (P2 (map(λ(str)(string-split str " ")) P1))
           (P3 (map(λ(lst)
                     (map(λ(str)
                           (let((n (string->number str)))
                             (if n n(string->symbol str))))lst))P2))
           (E(map(λ(lst)(cadr lst))P3))
           (S(diferencia(cdar P3)E))
           (e0(cadar(filter(λ(lst)(en?(car lst)'(>> *> >*)))P3)))
           (A (map(λ(lst)(cadr lst))
                  (filter (λ(lst)(en?(car lst)'(** *> >*)))P3)))
          ; (T (append (map(λ(e) (drop e 3))P3) (map(λ(e) (drop-right(drop e 1)2))P3)))
           (T (append (map(λ(e) (append(drop (drop-right e 2)1)))P3) (map(λ(e)
                                                     (append(drop-right(drop e 1)
                                                                4)(drop e 4))
                                                            )P3)))
           )
      (list E S e0 A T))))

;'(0 q0 q3 1 q1 2 q6 q7 q1)-->'((0 q0)(0 q3)(1 q1) (2 q2)(2 q7)(2 q1))
(define split-at-symb
  (λ(lst S[res'()][tr'()])
    (cond((empty? lst)
          (append* (map(λ(tr)
                         (map(λ(ef)(list(car tr)ef))(cdr tr)))res)))
         ((en? (car lst)S)
          (split-at-symb(cdr lst)S
                        (cons(cons(car lst)tr)res))
          )                                        
         (else(split-at-symb(cdr lst)S res(cons(car lst)tr))))))

;'(>> q0 0 q0 1 q1 2 q6)--> '((q0 0 q0)(q0 1 q1)(q0 2 a6))
(define lst->tr
  (λ(lst S)
    (let* ((eini (cadr lst))
          (rst (cddr lst))
          (trs (split-at-symb(reverse rst)S))
    )
       (map(λ(t)(cons eini t))trs)
       )))
  


;define como a2 al afd leido del archivo .dat
;(define a2 (file->afd "a.dat"))

;muestra los datos del automata en forma de lista
;(info-afd)-->lista
;afd: %afd
(define info-afd
  (λ(afd)
    (list (send afd get-E)(send afd get-S)(send afd get-e0)(send afd get-A)(send afd get-T))))

;(define a4 (file->afd "04.dat"))

;lengujade de todas las palabras
;toda palabra que tenga a esta entre 2 b's
;expresion regular
;definicion del automata
;grafo de transiciones
;lenguaje de todas las palabras hasta longitud 6
;a242a

;(define a242a (file->afd "a242a.dat"))
;(define a242b (file->afd "a242b.dat"))

;AF->gv

;importa un archivo de texto ala memoria que contenga los datos correspondientes a un AFD
;(file->afd)-->%afd
;nomarch : nombre del archivo
(define file->afd
  (λ (nomarch)
    (let*((adf0 (file->lines nomarch))
          (adfe0 (map (λ (str) (map (λ (s) (let ((sy (string->number s))) (if sy sy (string->symbol s))))
                           (string-split str " ")))
                    adf0))
          (adf1 (map (λ (adf) (cdr adf))
                     (map (λ (str) (map (λ (s) (let ((sy (string->number s))) (if sy sy (string->symbol s))))
                           (string-split str " ")))
                    adf0)))
      (Estados (map (λ (a)
                      (car a))
                    adf1))
      (Leng (car (map (λ (l)
                        (filter-not (λ (s) (en? s Estados)) (cdr l)))
                      adf1))) 
      (E0 (car (map (λ (e)
                      (cond ((o (equal? (car e) '>>) (equal? (car e) '*>)) (cadr e))))
                    adfe0))) 
      (Aceptores (filter-not (λ (s)
                               (equal? s "neg"))
                             (map (λ (e)
                                    (cond ((o (equal? (car e) '**)
                                              (equal? (car e) '*>))(cadr e))
                                          (else "neg"))) adfe0)))
      (T (append* (map (λ(e la)
                          (creaT e (cdr la)))
                       Estados adf1)))
      )
      ((λ (x) x) (new afd% [AF-conf (list Estados Leng E0 Aceptores T)])))
      )) 

(define af->gv
  (λ (AF [nomarch "nomarch"]
        #:motor [motor "dot"])
    (let* ((nomin (string-append nomarch ".gv"))
           (nomout2 (string-append nomarch ".png"))
           (out (open-output-file nomin #:mode 'text #:exists 'replace))
           (E (send AF get-E));estados del AF
           (e0 (send AF get-e0));estado inicial del AF
           (A (send AF get-A));aceptores
           (T (send AF get-T
                      ));transiciones
           )
      (fprintf out "digraph {~n")
      (fprintf out "rankdir = LR~n")
      ;dibuja nodos y aristas con colores
      (fprintf out "AF [shape = none, color = blue, fontcolor = white] ~n") ;crea nodo que apunta al estado inicial
      
      (for-each (λ (e) (fprintf out "~s [shape = circle, color = ~a] ~n" (cond ((list? e) (string-append*
                                                                                           (map (λ (es) (symbol->string es)) e)))
                                                                               (else e))
                                                                                "black")) E);estados del AF
   
      (for-each (λ (a) (fprintf out "~s [shape = circle, color = ~a] ~n"(cond ((list? a) (string-append*
                                                                                           (map (λ (es) (symbol->string es)) a)))
                                                                               (else a))
                                "green")) A);aceptores del AF
      (for-each (λ (t) (fprintf out "~s -> ~s [label = ~s]; ~n" (cond ((list? (car t)) (string-append*
                                                                                           (map (λ (es) (symbol->string es)) (car t))))
                                                                               (else (car t)))
                                                                 (cond ((list? (last t)) (string-append*
                                                                                           (map (λ (es) (symbol->string es)) (last t))))
                                                                               (else (last t)))                                                               
                                                                 (second t))) T);transicion del AF
      (fprintf out "AF -> ~s [label = Inicio, color = blue] ~n" e0) ;apunta al estado inicial
      (fprintf out "}~n")
      (close-output-port out)
      (system (string-append motor " " nomin " -Tpng -o " nomout2))
      (bitmap/file nomout2)
)))

;================================================================================================
(define te '(1 2 3))
(define tl '(A B))
(define tei '1)
(define ta '(2))
(define tt '((1 A 2)(2 B 1)(1 B 3)(2 A 3)(3 A 3)(3 B 3)))
(define atest(new afd%[AF-conf(list te tl tei ta tt)])) 

;EXAMEN 1ER PARCIAL
;1)
;definicion del automata del 1er inciso
;(define am (file->afd "at1.dat"))

;2)
;cambia el estado inicial de un AFD
;af : automata finito
;e : estado
;(cambia-e0) --> %afd si es elemento de E, #f en otro caso
(define cambia-e0 
  (λ(af e)
    (if(en? e (send af get-E))
     (new afd%[AF-conf(list (send af get-E)(send af get-S) e (send af get-A) (send af get-T))])
     #f
    )))

;3)
;metodo auxiliar parar
;crea la familia del AFD
;(xfam af)-> lista de %afd     #incluye el primer elemento, la funcion principal lo quita
;af : automata finito
;x : cantidad de estados
;res : contiene la lista de automatas
(define xfam
  (λ(af x res)
    (cond((equal? x 0)res)
         (else (xfam af (- x 1)
                     (append res (list(new afd%[AF-conf(list (send af get-E)(send af get-S) x (send af get-A) (send af get-T))])))
                     ))
  )))

;crea la familia del AFD
;(fam af)->lista de %afd
;af : automata finito
(define fam
  (λ(af)
    (drop-right(xfam af (card(send af get-E)) '())1)))

;4)
;El lenguaje nuclear de un automata
;Es el conjunto de palabras que son aceptadas por todos los miembros de la familia de un automatada
;(Lnuclear F)--> lenguaje
;F : familia de automatas
(define Lnuclear
  (λ(F)
    (append*(map (λ(a)(send a lenguaje))
                    F))
    ))


;=========================================================================================
;SEGUNDO PARCIAL AUTOMATAS
;=========================================================================================
;======================================================================================
;AUTOMATAS FINITOS NO-DETERMINISTAS
;======================================================================================
(define afn%
  (class object%  
  (init AF-conf)                ; initializacion de argumentos
  (define E (list-ref AF-conf 0));estado
  (define S (list-ref AF-conf 1));alfabeto
  (define e0 (list-ref AF-conf 2));estado inicial
  (define A (list-ref AF-conf 3));aceptores
  (define T (list-ref AF-conf 4));tranciciones
    (super-new)
    (define/public(get-E) E)
    (define/public(get-S) S)
    (define/public(get-e0) e0)
    (define/public(get-A) A)
    (define/public(get-T) T)
    ;afd
    ;(send atestn tran 2 'A)-->3
    ;afn
    ;(send atestn tran 2 'A)--'(3)
    (define/public(tran e s)
      (map(λ(e)(last e));obtengo los estados de las transiciones que me da
          (filter(λ(t);filtrame las que no cumplen con
                        (equal? (list e s)(drop-right t 1))) ;esta condicion
                     T;tomadas de este conjunto
                      )))

    ;generalizacion de la transicion
    ;determina el estado final al que AFD accede despues de leer todas las letras de la palabra
    ;desde un estado inicial dado
    ;(tran* a1 1 '(C C C A C C) --> 1
    ;tran*
    (define/public (tran* cje s)
    (if (pVacia? s)
        (list cje)
        (append* (map (λ (e) (tran* e (cdr s))) (tran cje (car s))))))

    (define/public (tran** cje s)
      (if (pVacia? s)
          cje
      (append* (map (λ (e) (tran e (cdr s))) (tran cje (car s))))))

       ;aceptor
    ;determina si el termino es un estado aceptor
    ;e : estado
    ;A : lista de estados aceptores
    ;(aceptor e)-->boleano
    ;(send a1 aceptor 'e1) --#t    ;(send a1 aceptor 'e5)-->#f
    (define/public (analiza w)
      (tran* e0 w))
    
    ;aceptada
    ;pregunta si la palabra es aceptada por lenguaje del automata
    ;--si al terminar la palabra esta en un estado aceptor o no--
    ;w : palabra
    ;(aceptada? w)-->boleano
    (define/public (acepta? w)
      (existe-Un
       (λ(ef)(en? ef A))(analiza w)))


));clase afn

;Importador de archivo de texto al racket de un AF
(define file->af
  (λ(nomarch)
    (let* ((P1 (file->lines nomarch #:mode 'text))
           (P2 (map(λ(str)(string-split str " ")) P1))
           (P3 (map(λ(lst)
                     (map(λ(str)
                           (let((n (string->number str)))
                             (if n n(string->symbol str))))lst))P2))
           (E(map(λ(lst)(cadr lst))P3))
           (S(diferencia(cdar P3)E))
           (e0(cadar(filter(λ(lst)(en?(car lst)'(>> *> >*)))P3)))
           (A (map(λ(lst)(cadr lst))
                  (filter (λ(lst)(en?(car lst)'(** *> >*)))P3)))
           (T (append*(map(λ(lst)(lst->tr lst S))P3)))
           )      
      (new (tipoAF T) [AF-conf (list E S e0 A T)])
      )
    ))
;devuelve el tipo del automata dato de acuerdo a su transicion
; (tipoAF a) --> afd% | afn%  afe%
(define tipoAF
  (λ (T)
    (cond ((esFun? T) afd%)
            (else afn%))))

(define ten '(1 2 3))
(define tln '(A B))
(define tein '1)
(define tan '(2))
(define ttn '((1 A 3)(1 A 2)(2 B 1)(1 B 3)(2 A 3)(3 A 3)(3 B 3)))
(define atestn(new afn%[AF-conf(list ten tln tein tan ttn)])) 
(define n02 (file->af "n02.dat"))

(define afn->afd
  (λ(N) ;<--nodo
    (let* ((conf (info-afd N))
           (EN (list-ref conf 0))
           (SN (list-ref conf 1))
           (e0N (list-ref conf 2))
           (AN (list-ref conf 3))
           (TN (list-ref conf 4))
           (ED (map(λ(e)(list e)) EN))
           (SD SN)
           (e0D (car(filter(λ(e)(en? e0N e)) ED)))
           (TD (append*(map(λ(e)
                     (map(λ(s)
                           (list(list e)
                                s
                                (send N tran e s)))
                         SN))
                   EN)))
           )
      (list ED SD e0D TD)
      )))



























