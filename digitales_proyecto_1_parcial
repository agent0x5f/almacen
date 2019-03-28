#programas del primer parcial de digitales advanzadas
#Version 2.0
#Miguel Mateo Hernandez Vargas
#son 3 tipos de sistemas MM1,MD1 y ME1
1;
warning('off', 'all');
#MM1
function val1 = Wmm(u,l)
resp = 1./(u-l);
val1 = resp;
endfunction

function val2 = WQmm(u,l)
resp = l./(u.*(u-l));
val2 = resp;
endfunction 

function val3 = Lmm(u,l)
resp = l./(u-l);
val3 = resp;
endfunction

function val4 = LQmm(u,l)
resp = (l*l)./(u.*(u-l));
val4 = resp;
endfunction

#MD1
function val5 = Wmd(u,l)
resp=(l./((u.*2).*(u-l)))+(u.^-1);
val5=resp;
endfunction

#tiempo de espera promedio en la cola
function val6 = WQmd(u,l)
resp=l./((u.*2).*(u-l)); 
val6=resp;
endfunction

#promedio de llamadas al sistema
function val7 = Lmd(u,l)
resp=((l*l)./((u.*2).*(u-l)))+(l./u);
val7=resp;
endfunction

#promedio de llamadas al sistema en la cola
function val8 = LQmd(u,l)
resp=(l.*l)./((2*u).*(u-l));
val8=resp;
endfunction

#ME1
function val9 = Wme(u,l,k)
resp = (((1+k)./(k.*2)).*(l./(u.*(u-l))))+(u.^-1);
val9 = resp;
endfunction

#tiempo de espera promedio en la cola
function val10 = WQme(u,l,k)
resp = ((1+k)./(k.*2)).*(l./(u.*(u-l)));
val10=resp;
endfunction

#promedio de llamadas al sistema
function val11 = Lme(u,l,k)
resp = (((1+k)./(k.*2)).*((l*l)./(u.*(u-l))))+(l./u);
val11=resp;
endfunction

#promedio de llamadas al sistema en la cola
function val12 = LQme(u,l,k)
resp = ((1+k)./(k.*2)).*((l*l)./(u.*(u-l)));
val12=resp;
endfunction


printf("\n");
printf("Sistemas de encolamiento: \n");
printf("1.-MM1\n");
printf("2.-MD1\n");
printf("3.-ME1\n");
printf("4.-Todos\n");
opcion = input("Opcion: ");

switch(opcion)
case 1
	printf("1.-W\n");
	printf("2.-WQ\n");
	printf("3.-L\n");
	printf("4.-LQ\n");
	opcion2 = input("Opcion: ");
	
	if(opcion2 == 1)
		l = input("lambda: ");
		z=0:0.1:10;
	
		printf("--------------------\n");
		printf("    u    |Wmm(u,l=%d)\n",l);
		printf("--------------------\n");
		for n=0:0.1:10
		h = Wmm(n,l);
		printf("%f | %f\n",n,h);
		end

		plot(z,Wmm(z,l));
		xlabel("u");
		ylabel("Wmm(u,lambda)");
		pause();
	elseif(opcion2 == 2)
		l = input("lambda: ");
		z = 0:0.1:10;
		
		printf("---------------------\n");
		printf("    u    |WQmm(u,l=%d)\n",l);
		printf("---------------------\n");
		for n=0:0.1:10
		h = WQmm(n,l);
		printf("%f | %f\n",n,h);
		end
		
		plot(z,WQmm(z,l));
		xlabel(" u ");
		ylabel("WQmm(u,lamda)");
		pause();
	elseif(opcion2 == 3)
		l = input("lambda: ");
		z = 0:0.1:10;
		
		printf("---------------------\n");
		printf("    u    |Lmm(u,l=%d)\n",l);
		printf("---------------------\n");
		for n=0:0.1:10
		h = Lmm(n,l);
		printf("%f | %f\n",n,h);
		end

		plot(z,Lmm(z,l));
		xlabel(" u ");
		ylabel("Lmm(u,lambda)");
		pause();
	elseif(opcion2 == 4)
		l = input("lambda: ");
		z = 0:0.1:10;
		
		printf("----------------------\n");
		printf("    u    |LQmm(u,l=%d)\n",l);
		printf("----------------------\n");
		for n=0:0.1:10
		h = LQmm(n,l);
		printf("%f | %f\n",n,h);
		end
	
		plot(z,LQmm(z,l));
		xlabel(" u ");
		ylabel("LQmm(u,lambda)");
		pause();		
	endif

case 2
	printf("1.-W\n");
	printf("2.-WQ\n");
	printf("3.-L\n");
	printf("4.-LQ\n");
	opcion3 = input("Opcion: ");
	
	if(opcion3 == 1)
		l = input("lambda: ");
		z=0:0.1:10;
	
		printf("--------------------\n");
		printf("    u    |Wmd(u,l=%d)\n",l);
		printf("--------------------\n");
		for n=0:0.1:10
		h = Wmd(n,l);
		printf("%f | %f\n",n,h);
		end

		plot(z,Wmd(z,l));
		xlabel("u");
		ylabel("Wmd(u,lambda)");
		pause();
	elseif(opcion3 == 2)
		l = input("lambda: ");
		z = 0:0.1:10;
		
		printf("---------------------\n");
		printf("    u    |WQmd(u,l=%d)\n",l);
		printf("---------------------\n");
		for n=0:0.1:10
		h = WQmd(n,l);
		printf("%f | %f\n",n,h);
		end
		
		plot(z,WQmd(z,l));
		xlabel(" u ");
		ylabel("WQmd(u,lamda)");
		pause();
	elseif(opcion3 == 3)
		l = input("lambda: ");
		z = 0:0.1:10;
		
		printf("---------------------\n");
		printf("    u    |Lmd(u,l=%d)\n",l);
		printf("---------------------\n");
		for n=0:0.1:10
		h = Lmd(n,l);
		printf("%f | %f\n",n,h);
		end

		plot(z,Lmd(z,l));
		xlabel(" u ");
		ylabel("Lmd(u,lambda)");
		pause();
	elseif(opcion3 == 4)
		l = input("lambda: ");
		z = 0:0.1:10;
		
		printf("----------------------\n");
		printf("    u    |LQmd(u,l=%d)\n",l);
		printf("----------------------\n");
		for n=0:0.1:10
		h = LQmd(n,l);
		printf("%f | %f\n",n,h);
		end
	
		plot(z,LQmd(z,l));
		xlabel(" u ");
		ylabel("LQmd(u,lambda)");
		pause();		
	endif

case 3
	printf("1.-W\n");
	printf("2.-WQ\n");
	printf("3.-L\n");
	printf("4.-LQ\n");
	opcion4 = input("Opcion: ");
	k = 3;
	
	if(opcion4 == 1)
		l = input("lambda: ");		
		z=0:0.1:10;
	
		printf("--------------------\n");
		printf("    u    |Wme(u,l=%d,k=%d)\n",l,k);
		printf("--------------------\n");
		for n=0:0.1:10
		h = Wme(n,l,k);
		printf("%f | %f\n",n,h);
		end

		plot(z,Wme(z,l,k));
		xlabel("u");
		ylabel("Wme(u,lambda)");
		pause();
	elseif(opcion4 == 2)
		l = input("lambda: ");
		z = 0:0.1:10;
		
		printf("---------------------\n");
		printf("    u    |WQme(u,l=%d,k=%d)\n",l,k);
		printf("---------------------\n");
		for n=0:0.1:10
		h = WQme(n,l,k);
		printf("%f | %f\n",n,h);
		end
		
		plot(z,WQme(z,l,k));
		xlabel(" u ");
		ylabel("WQme(u,lamda)");
		pause();
	elseif(opcion4 == 3)
		l = input("lambda: ");
		z = 0:0.1:10;
		
		printf("---------------------\n");
		printf("    u    |Lme(u,l=%d,k=%d)\n",l,k);
		printf("---------------------\n");
		for n=0:0.1:10
		h = Lme(n,l,k);
		printf("%f | %f\n",n,h);
		end

		plot(z,Lme(z,l,k));
		xlabel(" u ");
		ylabel("Lme(u,lambda)");
		pause();
	elseif(opcion4 == 4)
		l = input("lambda: ");
		z = 0:0.1:10;
		
		printf("----------------------\n");
		printf("    u    |LQme(u,l=%d,k=%d)\n",l,k);
		printf("----------------------\n");
		for n=0:0.1:10
		h = LQme(n,l,k);
		printf("%f | %f\n",n,h);
		end
	
		plot(z,LQme(z,l,k));
		xlabel(" u ");
		ylabel("LQme(u,lambda)");
		pause();		
	endif

case 4
	printf("1.-W\n");
	printf("2.-WQ\n");
	printf("3.-L\n");
	printf("4.-LQ\n");
	opcion5 = input("Opcion: ");
	k = 3;
	
	if(opcion5 == 1)
		l = input("lambda: ");		
		z=0:0.1:10;
	
		printf("---------------------------------------------------\n");
		printf("    u    |Wmm(u,l=%d) |Wmd(u,l=%d)|Wme(u,l=%d,k=%d)\n",l,l,l,k);
		printf("---------------------------------------------------\n");
		for n=0:0.1:10
		mem1 = Wmm(n,l);
		mem2 = Wmd(n,l);
		mem3 = Wme(n,l,k);
		printf("%f | %f | %f | %f\n",n,mem1,mem2,mem3);
		end

		hold on;
		plot(z,Wmm(z,l));
		plot(z,Wmd(z,l));		
		plot(z,Wme(z,l,k));
		
		xlabel("u");
		ylabel("f(x)");
		pause();
	elseif(opcion5 == 2)
		l = input("lambda: ");
		z = 0:0.1:10;
		
		printf("---------------------------------------------------\n");
		printf("    u    |WQmm(u,l=%d)|WQmd(u,l=%d)|WQme(u,l=%d,k=%d)\n",l,l,l,k);
		printf("---------------------------------------------------\n");
		for n=0:0.1:10
		mem1 = WQmm(n,l);
		mem2 = WQmd(n,l);
		mem3 = WQme(n,l,k);
		printf("%f | %f | %f | %f\n",n,mem1,mem2,mem3);
		end
		hold on;
		plot(z,WQmm(z,l));
		plot(z,WQmd(z,l));		
		plot(z,WQme(z,l,k));
		xlabel("u");
		ylabel("f(x)");
		pause();
	elseif(opcion5 == 3)
		l = input("lambda: ");
		z = 0:0.1:10;
		
		printf("---------------------------------------------------\n");
		printf("    u    |Lmm(u,l=%d) |Lmd(u,l=%d)|Lme(u,l=%d,k=%d)\n",l,l,l,k);
		printf("---------------------------------------------------\n");
		for n=0:0.1:10
		mem1 = Lmm(n,l);
		mem2 = Lmd(n,l);
		mem3 = Lme(n,l,k);
		printf("%f | %f | %f | %f\n",n,mem1,mem2,mem3);
		end

		hold on;
		plot(z,Lmm(z,l));
		plot(z,Lmd(z,l));		
		plot(z,Lme(z,l,k));
		xlabel(" u ");
		ylabel("f(x)");
		pause();
	elseif(opcion5 == 4)
		l = input("lambda: ");
		z = 0:0.1:10;	

		printf("---------------------------------------------------\n");
		printf("    u    |LQmm(u,l=%d)|LQmd(u,l=%d)|LQme(u,l=%d,k=%d)\n",l,l,l,k);
		printf("---------------------------------------------------\n");
		for n=0:0.1:10
		mem1 = LQmm(n,l);
		mem2 = LQmd(n,l);
		mem3 = LQme(n,l,k);
		printf("%f | %f | %f | %f\n",n,mem1,mem2,mem3);
		end

		hold on;
		plot(z,LQmm(z,l));
		plot(z,LQmd(z,l));		
		plot(z,LQme(z,l,k));
		xlabel(" u ");
		ylabel("f(x)");	
		pause();
	endif

endswitch

