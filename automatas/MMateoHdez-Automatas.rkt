#lang racket
(require racket/trace
         racket/system
         racket/file
         racket/format
         2htdp/image)
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
  (λ (R #:D [D (Dominio R)])
    (para-Todo (λ (im) (= (card im)1))
              (map (λ (x) (Imagen x R)) D))))

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
    (define/public (get-Conf) (list E S e0 A T))
    ;transicion
    ;obtener el siguiente estado al que accede el AFD
    ;(tran e s) --> e
    ;e : estado
    ;s : letra
   (define/public(tran e s)
            (let ((p (filter(λ(t);filtrame las que no cumplen con
                        (equal? (list e s)(drop-right t 1))) ;esta condicion
                     T;tomadas de este conjunto
                      )))
            (if(empty? p)'() ;fue a una transicion noexistente
               (last(car p)
            ))))

    ;generalizacion de la transicion
    ;determina el estado final al que AFD accede despues de leer todas las letras de la palabra
    ;desde un estado inicial dado
    ;(tran* a1 1 '(C C C A C C) --> 1
    ;tran*
    ;NOTA: funciona solo con los simbolos separados por espacios '(0 1 0)
    (define/public (tran* edo palabra)
      (if (pVacia? palabra)
          edo
          (tran* (tran edo (car palabra)) (cdr palabra))
          ))
  
    (define/public (tranz L1 S)          
    (filtrado(append*(map(λ(l)(if (symbol?(tran l S))
                                  (list (tran l S))
                                  (tran l S)))L1))))
    
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
    ;me da todos los estados a los que esta apuntando q
    ;q :estado
    (define/public(dq q)
      (remove* '(())
       (remove-duplicates (append*
         (map(λ(e)(if(en? q e)
                        (map(λ(s)(tran q s))S)
                        '()
                        ))
             T)))))

;transiciones, hacia 2 nodos
       (define/public (trany** cje s)
      (if (pVacia? s)
          cje
    (map(λ(e)(list(list (car e)(car(cdr e))(car(cdr (cdr e))))
                  (list (car e)(car(cdr e))(car(cdr (cdr (cdr e))))
                  )))
          (remove* '(1) (append*(map(λ(l) (map (λ (e)
                      (cond((=(card (tran e l))0)1)
                           ((=(card (tran e l))2)(append (list e)(list l)
                          (tran e l)))
                           (else
                       1))                         
                      )
                    cje))s))
      )))) 
));clase afd

(define creaT
  (λ (e la [t '()])
    (if (empty? la) t
    (creaT e (drop la 2) (agregar (cons e (take la 2)) t)))))

(define diferencia
  (λ (A B)
    (filter-not (λ (a) (en? a B)) A)))   

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

;muestra los datos del automata en forma de lista
;(info-afd)-->lista
;afd: %afd
(define info-afd
  (λ(afd)
    (list (send afd get-E)(send afd get-S)(send afd get-e0)(send afd get-A)(send afd get-T))))

(define af->gv
  (λ (AF [nomarch "nomarch"]
        #:motor [motor "dot"])
    (let* ((nomin (string-append nomarch ".gv"))
           (nomout2 (string-append nomarch ".png"))
           (out (open-output-file nomin #:mode 'text #:exists 'replace))
           (E (send AF get-E));estados del AF
           (e0 (send AF get-e0));estado inicial del AF
           (A (send AF get-A));aceptores
           (T (send AF get-T));transiciones
           )
      (fprintf out "digraph {~n")
      (fprintf out "rankdir = LR~n")
      ;dibuja nodos y aristas con colores
      (fprintf out "AF [shape = none, color = blue, fontcolor = white] ~n") ;crea nodo que apunta al estado inicial
      
      (for-each (λ (e) (fprintf out "~s [shape = circle, color = ~a] ~n" (cond ((list? e) (string-append*
                                                                                           (map (λ (es) (symbol->string es)) e)))
                                                                               (else e))
                                                                                "black")) E);estados del AF
   
      (for-each (λ (a) (fprintf out "~s [shape = doublecircle, color = ~a] ~n"(cond ((list? a) (string-append*
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
      (fprintf out "AF -> ~s [label = Inicio, color = blue] ~n" (cond ((list? e0) (string-append*
                                                                                           (map (λ (es) (symbol->string es)) e0)))
                                                                               (else e0))) ;apunta al estado inicial
      (fprintf out "}~n")
      (close-output-port out)
      (system (string-append motor " " nomin " -Tpng -o " nomout2))
      (bitmap/file nomout2)
)))

;================================================================================================
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
 (define info-afn
  (λ(afn)
    (list (send afn get-E)(send afn get-S)(send afn get-e0)(send afn get-A)(send afn get-T))))

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

    ;(tranz '(b z) '1))-->'(b c d)
(define/public (tranz L1 S)          
    (append*(map(λ(l)(tran l S))L1)
          ))

    ;generalizacion de la transicion
    ;determina el estado final al que AFD accede despues de leer todas las letras de la palabra
    ;desde un estado inicial dado
    ;(tran* a1 1 '(C C C A C C) --> 1
    ;tran*
    (define/public (tran* cje s)
    (if (pVacia? s)
        (list cje)
        (append* (map (λ (e) (tran* e (cdr s))) (tran cje (car s))))))

    ;me regresa una lista que contiene las estados por columna
    (define/public (tran** cje s)
      (if (pVacia? s)
          cje
      (list*(map(λ(l) (map (λ (e)
                      (if (en? e E)
                          (tran e l)
                          ('()))
                      )
                    cje))s))
      ))
;transiciones,deben ser convertidad aforma  standart con hacia 0 nodos
       (define/public (tranx** cje s)
      (if (pVacia? s)
          cje
     (append*(map(λ(l) (map (λ (e)                       
                       (append (list e)(list l)
                          (tran e l))                   
                      )
                    cje))s))
      ))

;transiciones, hacia 2 nodos
       (define/public (trany** cje s)
      (if (pVacia? s)
          cje
    (map(λ(e)(list(list (car e)(car(cdr e))(car(cdr (cdr e))))
                  (list (car e)(car(cdr e))(car(cdr (cdr (cdr e))))
                  )))
          (remove* '(1) (append*(map(λ(l) (map (λ (e)
                      (cond((=(card (tran e l))0)1)
                           ((=(card (tran e l))2)(append (list e)(list l)
                          (tran e l)))
                           (else
                       1))                         
                      )
                    cje))s))
      )
          )
          ))

(define/public (lst->one lst [res '()])
  (if(empty? lst)
     res
     (lst->one (cdr lst)(append res (car lst))
     )))
    ;detecta los nuevos estados
    ;'(a b c)->'((a)(b)(c))
(define/public (lst->lst* lst [res '()])
  (if(empty? lst)
     res
     (lst->lst* (cdr lst)(append res (list(list (car lst) ))))))

    (define/public (lst->lst** lst [res '()])
  (if(empty? lst)
     res
     (lst->lst* (cdr lst)(append res (list (car lst) )))))

;(send ntestd detector(send ntestd lst->one
    ;(send ntestd tran** '(a b c d)'(0 1))))
    ;'(() (b d) (b c) (c d))
    ;me dice las nuevas estados
(define/public (detector lst)
  (remove-duplicates(remove* (lst->lst** E) lst)))

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

    ;lenguaje del afn
    ;produce el conjunto de palabras aceptadas por el automata
    ;(leng [k])-->lenguaje
    ;k : entero mayor o igual que cero
    (define/public (lenguaje [k 6])
      (let ((S* (nKleene S k)))
        (filter (λ (w) (acepta? w)) S*)))
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
          ((existe-Un(λ(t)(en? 'Z t))T) afe%)  ;bug con el caso de 1 sola transicion          
            (else afn%))))

(define crea-tran
 (λ(afn)
   (send afn lst->one(send afn trany** (send afn get-E)(send afn get-S)))))

;tarea 2.04
;obtener los estados accesibles de un automata dado
;(accesibles afd)-->lst
;afd : automata finito determinista
(define accesibles
  (λ(afd Q [acc '()])
    (if(empty? Q)
       (remove-duplicates acc)
       (accesibles afd
                   (append(cdr Q)
                         (diferencia(diferencia (send afd dq(car Q))
                                      Q)acc))
                   (append Q acc)
                  )
       )))

(define inaccesibles
  (λ(afd)
    (diferencia (send afd get-E)(accesibles afd (list(send afd get-e0)) ))))

;tarea 2.05
;crear un automata sin estados inalcanzables apartir de uno que si los tiene
;(reduce afd)-->afd%
;afd : automata finito determinista
(define reduce-ina
  (λ(afd)
     (let* (
            (EN (accesibles afd (list(send afd get-e0))))
           (SN (send afd get-S))
           (e0N (send afd get-e0))
           (AN (interseccion EN (send afd get-A)))
           (TN (append*(map(λ(e)
                             (map(λ(s)(append (cons e(list s)) (list(send afd tran e s))))
                                 SN))
                           EN)))            
                  )
      (new afd%[AF-conf(list EN SN e0N AN (reduce-aux TN))])     
       )))

(define reduce-aux
  (λ(T [res '()])
    (if(empty? T)
       (remove* '((*)) res)
    (reduce-aux (cdr T)(append(list(if(empty? (last (car T)))                          
                           '(*)
                           (car T)
                           ))
                           res)))))

;convertidor 2.0
;debo corregir, revisar el vacio y el hacia nada
(define afn->afd
  (λ(N [n 0]) ;<--afn
    (let* ((conf (info-afd N))
           (E (list-ref conf 0))
           (S (list-ref conf 1))
           (e0 (list-ref conf 2))
           (A (list-ref conf 3))
           (T '())
           (Es (filtrado(map(λ(e)   ;'(1 2 3)-->'((1)(2)(3))
                     (if(neg(list? e))(list e)e))E)))
           (Ex (filtrado(append*(map(λ(e)  ;genera nuevos e, puede tener viejos
                     (map(λ(s)
                           (send N tranz (if(neg(list? e))
                                            (list e)
                                            e)
                                 s)
                                )S))E))))
           (Tx                    ;genera nuevos t, puede tener viejos
            (filter (λ(w)(> (length w) 2))
            (append*
                  (map(λ(e)             
                     (map(λ(s)
                           (append* (list e) (list s)
                               (list(filtrado(list(send N tranz(if(neg(list? e))
                                                         (list e)e) s))))                                    
                                    ))S))(filtrado E))
                        )))           
           (Exu(filtrado(diferencia Ex Es)))
           (As (map(λ(e)
                     (if(neg(list? e))(list e)e))A))
           (Axu(append* (map(λ(l)
                      (filter(λ(e)                             
                               (en? l (if(neg(list? e))
                                         (list e)
                                         e)
                                    ))Exu))A)))
           (afx (new afd%[AF-conf(list(union E Exu)S e0 (union A Axu)(union T Tx))])) 
      )
    (if (empty? Exu)
    afx
   (afn->afd afx (+ n 1)))
    )))
;====================================================================================================
;crea una funcion homomorfa
(define crea-He
  (λ (E #:prf [prf ' q])
    (let* ((k (card E))
           (indices (build-list k values))
           (nE (map (λ (i) (string->symbol(format "~s~s" prf i)))
                    indices))
           )
      (λ (q)
      (let ((v(assoc q (map (λ (e ne)(list e ne))E nE)))
            )
        (if v (last v) v)))
      )
    ))

;; (renombra-e af #:prf [prf ' q]) --> afd || afn
;; funcion que hace el renombramiento de los estados del automata finito
;; af --> automata finito
(define renombra-e
  (λ (af #:prf [prf ' q])
    (let* ((conf (info-afn af))
           (E(car conf))
           (fH (crea-He E #:prf prf)) ;<-- los estados
           (nE (map (λ (e) (fH e))E))
           (S (list-ref conf 1)); <-- mismos simbolos
           (e0 (fH(list-ref conf 2))) ;<-- el estado inicial
           (A (map (λ (e) (fH e))(list-ref conf 3))) ; <-- los aceptores
           (T (map (λ (t) (list (fH (car t))
                                (cadr t)
                                (fH (last t))))
                   (list-ref conf 4)
                   ))
           )
      (new (tipoAF T)[AF-conf(list nE S e0 A T)])
      )))

;=========================================================================================
;TERCER PARCIAL AUTOMATAS
;=========================================================================================
;======================================================================================
;AUTOMATAS FINITOS NO-DETERMINISTAS CON TRANSICIONES EPSILON
;======================================================================================
 (define info-afe
  (λ(afe)
    (list (send afe get-E)(send afe get-S)(send afe get-e0)(send afe get-A)(send afe get-T))))

(define afe%
  (class object%  
  (init AF-conf)                ; initializacion de argumentos
  (define E (list-ref AF-conf 0));estado
  (define S (list-ref AF-conf 1));alfabeto
  (define e0 (list-ref AF-conf 2));estado inicial
  (define A (list-ref AF-conf 3));aceptores
  (define T (list-ref AF-conf 4));tranciciones
    (super-new)
    (define/public(get-E) E)
    (define/public(get-S) (diferencia S '(Z)))
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

    ;eCerr
    ;obtien la cerradura epsilon de un estado
    ;(eCerr e)->conj estados
    ;e : estado
    (define/public (eCerr e)
      (let((P (tran e 'Z )))
        (if(empty? P)
           (list e)
           (append* (cons(list e)(map(λ(p)(eCerr p))P))))))

    ;tarea 3.03
    ;ontiene la transicion de estados, variante epsilon
    ;(trane e s)-->conj estados
    ;e : estado
    ;s : un simbolo
    (define/public (trane e s)
      (let((Q (eCerr e)))
        (let ((R(append* (map(λ(e)(tran e s))Q))))
          (append*(map(λ(e)(eCerr e))R))
      )))

    ;solo para afe
    ;me da solo a transicion real con el simbolo
    ;elimina las Z
    ;supondre que el ultimo estado es el real
    (define/public (ftr e s)
      (if(and
          (list?(trane e s))
          (neg(empty?(trane e s))))
          (last (trane e s))
          (trane e s)))

    (define/public (tranz L1 S)          
    (filtrado(append*(map(λ(l)(tran l S))L1)
          )))

    ;generalizacion de la transicion
    ;determina el estado final al que AFD accede despues de leer todas las letras de la palabra
    ;desde un estado inicial dado
    ;(tran* a1 '1 '(C C C A C C)) --> 1
    ;tran*
    (define/public (tran* cje s)
    (if (pVacia? s)
        (list cje)
        (append* (map (λ (e) (tran* e (cdr s))) (trane cje (car s))))))

    ;me regresa una lista que contiene las estados por columna
    (define/public (tran** cje s)
      (if (pVacia? s)
          cje
      (list*(map(λ(l) (map (λ (e)
                      (if (en? e E)
                          (trane e l)
                          ('()))
                      )
                    cje))s))
      ))
;transiciones,deben ser convertidad aforma  standart con hacia 0 nodos
       (define/public (tranx** cje s)
      (if (pVacia? s)
          cje
     (append*(map(λ(l) (map (λ (e)                       
                       (append (list e)(list l)
                          (trane e l))                   
                      )
                    cje))s))
      ))
;transiciones, hacia 2 nodos
       (define/public (trany** cje s)
      (if (pVacia? s)
          cje
    (map(λ(e)(list(list (car e)(car(cdr e))(car(cdr (cdr e))))
                  (list (car e)(car(cdr e))(car(cdr (cdr (cdr e))))
                  )))
          (remove* '(1) (append*(map(λ(l) (map (λ (e)
                      (cond((=(card (trane e l))0)1)
                           ((=(card (trane e l))2)(append (list e)(list l)
                          (trane e l)))
                           (else
                       1))                         
                      )
                    cje))s))
      ))))

(define/public (lst->one lst [res '()])
  (if(empty? lst)
     res
     (lst->one (cdr lst)(append res (car lst))
     )))
    ;detecta los nuevos estados
    ;'(a b c)->'((a)(b)(c))
(define/public (lst->lst* lst [res '()])
  (if(empty? lst)
     res
     (lst->lst* (cdr lst)(append res (list(list (car lst) ))))))

    (define/public (lst->lst** lst [res '()])
  (if(empty? lst)
     res
     (lst->lst* (cdr lst)(append res (list (car lst) )))))

;(send ntestd detector(send ntestd lst->one
    ;(send ntestd tran** '(a b c d)'(0 1))))
    ;'(() (b d) (b c) (c d))
    ;me dice las nuevas estados
(define/public (detector lst)
  (remove-duplicates(remove* (lst->lst** E) lst)))

       ;analiza
    ;me dice el estado al que llega la palabra
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
    
));clase afe

;convertidor de un automata finito epsilon a uno no determinista
(define afe->afn
  (λ(afe)
    (let* ((E(send afe get-E))
          (S(send afe get-S))
          (e0(send afe get-e0))
          (A(send afe get-A))
          (T(append*(map(λ(e)
            (append*(map(λ(s)
            (map(λ(q)
            (list e s q))(send afe trane e s)))
                          S)))
                          E)))
            )
          (new afn%[AF-conf(list E S e0 A T)])
          )))

;'((1 2 4)(2 4 1)(1 2 3))-->'((1 2 4)(1 2 3))
(define filtrado
  (λ(L)
    (if (list? L)
    (map(λ(k)(if(=(card k)1)(car k)k))
 (if (list? (map(λ(e)(ordena e))(sym->lst L)))
     (remove-duplicates(map(λ(e)(ordena e))(sym->lst L)))
     (map(λ(e)(ordena e))(sym->lst L))
    ))L)
    )
  )

;ordena listas con simbolos o numeros pero no con ambos
;(ordena '(D J A))-->'(A D J)
;(ordena '(8 3 2))-->'(2 3 8)
(define ordena
  (λ(L)
    (cond((empty? L)L)
      ((number?(car L))(sort L <))        
       (else(map(λ(e)(string->symbol e))
       (sort (map(λ(e)(symbol->string e))L) string<?))))))

(define sym->lst
 (λ(L)
   (map(λ(k)(if (symbol? k)(list k)k))L)))

;reductor de estados por clases de equivalencia
(define subconjunto?
  (λ (A B)
    (cond ((vacio? A) #t)    
    ( (en? (car A) B ) (subconjunto? ( cdr A) B ))
     ( else #f))))

; C=? ---> Booleano
; A : Conjunto
; B : Conjunto
(define C=?
  (λ (A B)
  ( y ( subconjunto? A B) (subconjunto? B A) )))

(define Eeqv?
  (λ (e1 e2 KK af)
    (andmap (λ (s)
           (ormap(λ (ki)
                   (and (en? (send af tran e1 s) ki)
                        (en? (send af tran e2 s) ki)))
                   KK))
                 (cadr (send af get-Conf)))))

(define splitK
  (λ (k kk af [res '()])
    (if (empty? k)
           res
           (let ((Eqv1 (filter (λ(q) (Eeqv? (car k) q kk af)) k)))                  
                  (splitK (diferencia k Eqv1) kk af (cons Eqv1 res))))))

(define splitKK
  (λ (kk Kref af [res '()])
    ;(printf "kk: ~a~n" kk)
    (if (empty? kk)
           res
           (splitKK (cdr kk) Kref af (append (splitK (car kk) Kref af) res)))))       

(define K0
  (λ (afd)
    (list (diferencia (send afd get-E)
                      (send afd get-A))
          (send afd get-A) )))

 (define nEq
   (λ (af [K (K0 af)] [Kant '()])
    ; (printf "nEq---~n")
     (if (C=? K Kant)
          K
         (nEq af (splitKK K K af) K))))

(define RedEeqv
  (λ (af)
    (let* ((conf (send af get-Conf))
           (SD (list-ref conf 1)) 
           (e0D (list-ref conf 2))
           (AD (list-ref conf 3))
           (E (nEq af))
           (S (list-ref conf 1))
           (e0 (car  (filter (λ (q) (en? e0D q)) E))) 
           (A  (filter-not (λ (q) (empty? (interseccion q AD))) E))
           (T (append* (map (λ (e)
                              (map (λ (s)
                                 (list e s (car (filter (λ (q)
                                                         (en? (send af tran (car e) s) q))
                                               E))))
                              S))
                       E)))
            )
     (new afd% [AF-conf (list E S e0 A T)])
      )))
;tarea 3.04
;(send(reduce-ina(renombra-e (RedEeqv (afn->afd(afe->afn (file->af "e01.dat.txt"))))))get-T)


;tarea 3.05
;union de af
;(af-union X Y)-->afe%
;X,Y : af
;no renombra los automatas
(define af-union
  (λ(X Y)
    (let*((J X)
          (K Y)
          (E (union*(send J get-E)(send K get-E)'(i)))
          (S (union(send J get-S)(send K get-S)))
          (e0 'i)
          (A (union(send J get-A)(send K get-A)))
          (T (union*(send J get-T)(send K get-T)
                    (list(list 'i 'Z (send J get-e0)))
                    (list(list 'i 'Z (send K get-e0))))))
    (new afe%[AF-conf(list E S e0 A T)]))))

;union de af
;(af-u X Y)-->afe%
;X,Y : af
;si renombra los automatas
(define af-u
  (λ(X Y)
    (let*((J (renombra-e #:prf 'A X))
          (K (renombra-e #:prf 'B Y))
          (E (union*(send J get-E)(send K get-E)'(i)))
          (S (union(send J get-S)(send K get-S)))
          (e0 'i)
          (A (union(send J get-A)(send K get-A)))
          (T (union*(send J get-T)(send K get-T)
                    (list(list 'i 'Z (send J get-e0)))
                    (list(list 'i 'Z (send K get-e0))))))
    (new afe%[AF-conf(list E S e0 A T)]))))

;union generalizada de af
;(af-union* LC )-->afe%
;LC: n af
(define af-union*
  (λ  LC    
    (define af-union*aux
      (λ (lc [res af-vacio])
        (if(vacio? lc)
           res
           (af-union*aux(cdr lc)(af-union(car lc)res)))))
(if(vacio? LC)
   af-vacio
   (af-union*aux LC))))

;af vacio
(define af-vacio
  (new afd%[AF-conf(list '(q0) '() 'q0 '() '())]))

;version 2 de la union generalizada de af
;(af-u* LC )-->afe%
;LC: n af
(define af-u*
  (λ LC
    (define af-u*aux
      (λ(lc lst[res af-vacio])
        (if(vacio? lc)
           res
           (af-u*aux(cdr lc)
                    (cdr lst)
   (af-union(renombra-e #:prf(car lst)(car lc))res))                    
           )));af-u*aux
(if(vacio? LC)
   af-vacio
   (af-u*aux LC
 (map(λ(i)(string->symbol
           (format "~s~s" 'N i)))
     (build-list (card LC) values))))))

;(define q1 (file->af "q1.dat.txt"))
;(define q2 (file->af "q2.dat.txt"))
;(define q3 (file->af "q3.dat.txt"))
;(define e01 (file->af "e01.dat.txt"))
;(af->gv (renombra-e(reduce-ina(afn->afd (afe->afn e01)))))
;(define ej1n (file->af "ej1.dat.txt"))
;(define ej1d (reduce-ina(afn->afd ej1n)))
;(define ej1dr (renombra-e ej1d))  ;convertido afn->afd :)
;(define ej2 (file->af "e2.dat.txt"))  ;para estados equivalentes

(define af-concat
  (λ(X Y)
   (let*((Xdef(renombra-e #:prf 'X X))
          (Ydef(renombra-e #:prf 'Y Y))
          (E(append '(i)(send Xdef get-E)(send Ydef get-E)))
          (S(union(send Xdef get-S)(send Ydef get-S)))
          (e0(send Xdef get-e0))
          (A(send Ydef get-A))
          (T(append(send Xdef get-T)
                   (send Ydef get-T)
                   (map(λ(a)(list a 'Z 'i))
                   (send Xdef get-A))
            (list(list 'i 'Z (send Ydef get-e0))))))
    (new afe% [AF-conf(list E S e0 A T)]))))                   

;generalizacion de la concanetacion de autommatas finitos
;(af-concat* LC)-->afe%
;LC : lista de automatas
(define af-concat*
  (λ  LC    
    (define af-concat*aux
      (λ (lc [res af-vacio])
        (if(vacio? lc)
           res
           (af-concat*aux(cdr lc)(af-concat(car lc)res)))))
(if(vacio? LC)
   af-vacio
   (af-concat*aux (cdr LC)(car LC)))))

;potencia de un automata finito
;(af-pot AF n)-->afe%
;AF: automata finito
;n entero positivo
(define af-pot
  (λ(X n [res af-pot0])
      (if(zero? n)
         res
         (af-pot X(- n 1)(af-concat res X)))
    ))

;(define p1(file->af "p1.dat.txt"))

(define af-pot0
  (new afe%[AF-conf(list '(q0 q1)
                         '()
                         'q0
                         '(q1)
                         '((q0 Z q1)(q0 Z q0)))]))
;segunda T para evitar bug de af->gv
;(send(reduce-ina(renombra-e (RedEeqv (afn->afd(afe->afn (file->af "e01.dat.txt"))))))get-T)

(define afe->afdm
  (λ(AFE)
    (reduce-ina(renombra-e(RedEeqv
                           (afn->afd(afe->afn AFE)))))))
(define afn->afdm
  (λ(AFN)
     (reduce-ina(renombra-e(RedEeqv
                            (afn->afd AFN))))))

;estrella de kleene para automatas finitos
(define af-*
  (λ(X)
    (let*((Xd (renombra-e #:prf 'X X))
           (E(append '(i f)(send Xd get-E)))
           (T(append (send Xd get-T)
    (map(λ(a)(list a 'Z 'f))(send Xd get-A))
    (list (list 'i 'Z(send Xd get-e0))
    '(f Z i)))))
(new afe%[AF-conf
    (list E (send Xd get-S)'i(cons 'f (send Xd get-A))T)]
     ))))

;genera un af que acepte la letra dada
;(genera-af 's)-->afd%
(define genera-af
  (λ(s)
    (new afe% [AF-conf(list '(q0 q1) (list s) 'q0 '(q1)
        ;                    (list(list 'q0 s 'q1)(list 'q0 'Z 'q0)))])))  ;af->gv t-fix
                            (list(list 'q0 s 'q1)))])))

;generalizacion de genera un af con una lista de letras
;(genera-af* Ls)-->afe%
;(genera-af* '(a b c))-->afe%
(define genera-af*
  (λ(Ls)
    (apply af-concat*(map(λ(s)(genera-af s))(reverse Ls)))))

;(Genera-af '((m a t e)(f i s i)(c o m p)))-->afe%
(define Genera-af
  (λ(Lw)
    (apply af-union*
    (map(λ(w)(genera-af* w))Lw))))

;(Genera-AF str)-->afe%
;(Genera-AF "hola")-->afe%
(define Genera-AF
  (λ(str)
    (genera-af*
    (map(λ(s)(let((tmp(string->number s)))
               (if tmp tmp(string->symbol s))))
    (map(λ(i)(substring str i (+ i 1)))
        (build-list(string-length str)values))))))
                                    
;(Genera-AF* "if" "else" "cond")-->afe%
(define Genera-AF*
  (λ LW
    (apply af-union*(map(λ(f)(Genera-AF f))LW))))

;=====EXAMEN 3ER PARCIAL==================================
;inciso A
;(define null (Genera-AF "N"));simbolo nulo
;(define num(lector "examena-d.txt"));{0...9} 
;(define e (Genera-AF "e"));simbolo de exponente
;(define p (genera-af "."));punto decimal
;(define n (genera-af "-"));signo negativo
;(define num* (af-* num));operador kleene
;(define x1 num*)
;(define x20 (af-concat* num* num p));concat* la entrada alrevez
;(define x2 (af-union null x20))
;(define x3 (af-union null e))
;(define x4 (af-union null n))
;(define x5 (af-concat* num* num))
;(define xx1 (af-concat* x5 x4 x3 x2 x1))
;(define x6 (af-union xx1 e))
;(define x7 x4)
;(define x8 x5)
;automata final sin reducir
;(define xx (af-concat* x8 x7 x6))
;============Respuesta final al punto A==================
;(define respA (afe->afdm xx)) ;descomentar xx
;========================================================
;ejemplos de numeros aceptados
;ejemplo de forma de uso
;(send(afe->afn (af-concat x1 x2))acepta? '(3 p 3 4))
;(send(afe->afn (af-concat x1 x2))acepta? n3)

;=============Respuesta al punto B=======================
;(define exa-b (lector "examenb.txt"))
;llamada en consola:
;(afe->afdm exa-b)
;========================================================

;=============Respuesta al punto C=======================
;(define exa-c1 (file->af "examenc.txt"))
;(define exa-c2(file->af "examenc-b.txt"))
;(define exa-c (af-concat exa-c1 exa-c2))
;AFD minimo Respuesta final
;(define respC (afe->afdm exa-c))

;====================================================================
;                          PROYECTO FINAL
;====================================================================

;hace una lista con todas las palabras que contiene el archivo
;(lector "archivo.txt")-->lst/palabras ;ej: '("uno" "dos" "tres")
;arch : string
(define lector
  (λ(arch)
       (flatten(map(λ(w)(string-split w))(file->lines arch)))))

;crea un afe que acepte las palabras que estan en el archivo
;(lector "index.txt")-->afe%
;arch : string
(define lector->afe
  (λ(arch)
    (let((af(apply af-u*(map(λ(w)(Genera-AF w))
       (lector arch)))))
    (new afe%[AF-conf(list (send af get-E)
                         (send af get-S)
                         (send af get-e0)
                         (send af get-A)
                         (remove '(i Z i)(send af get-T)))]))))

;crea una lista de simbolos o numeros a partir de un string
;(str->lst "hola")-->'(h o l a)
;str : string
(define str->lst
  (λ(str)
    (map(λ(s)(let((tmp(string->number s)))
               (if tmp tmp(string->symbol s))))
    (map(λ(i)(substring str i (+ i 1)))
        (build-list(string-length str)values)))))

;funcion auxiliar
;#t si todas las palabras en el archivo son aceptadas por el automata
;#f en otro caso
;Y : un archivo 
;X : automata con entrada del diccionario
;(final-aux dic arc)-->#boolean
(define final-aux
  (λ(X Y)
    (andmap(λ(i)(send X aceptada? (str->lst i)))
        (flatten(map(λ(w)(string-split w))(file->lines Y))))))

;definicion del diccionario con las palabras aceptadas
(define dic (afe->afdm(lector->afe "index.dat")))

;interfaz de final-aux
;#t si todas las palabras en el archivo son aceptadas por el diccionario
;#f en otro caso
;Z : un archivo
;(final "archivo.txt")-->#boolean
(define final
  (λ(Z)
    (final-aux dic Z)))

;FORMA DE USO
;Para cambiar el nombre del diccionario:
;    Cambiar en la definicion de dic el string "index.dat" por el nombre del archivo que usará
;Para probar si funciona el programa:
;    Llamar a final, ej: (final "archivo.txt")