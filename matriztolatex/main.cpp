#include <iostream>
#include <array>
#include <math.h>
#include <vector>
#include <string.h>
#include <fstream>
using namespace std;

class matrix{
public:
    matrix(unsigned,unsigned,int);
    matrix();
    matrix operator + (matrix&);
    matrix operator - (matrix&);
    matrix operator * (matrix&);
    matrix transpuesta ();
    matrix operator + (int&);
    matrix operator - (int&);
    matrix operator * (int&);
    matrix operator / (int&);
    int& operator ()(const unsigned&, const unsigned&);
    void print() const;
    unsigned get_filas() const;
    unsigned get_columnas() const;
    tuple<matrix,int,int>power_iter(unsigned,int);
    matrix deflacion(matrix&, int&);
    matrix(unsigned,unsigned,int**);
    void print_latex(int m,int n);
private:
    unsigned num_filas;
    unsigned num_columnas;
    vector<vector<int>> m_matrix;
};

matrix::matrix(unsigned tam_filas, unsigned tam_columnas,int inicial)
{
    num_filas = tam_filas;
    num_columnas = tam_columnas;
    m_matrix.resize(tam_filas);
    for(unsigned i=0;i<m_matrix.size();i++)
        m_matrix[i].resize(tam_columnas,inicial);
}

int& matrix::operator()(const unsigned &nfilas, const unsigned & ncolumnas)
{
    return this->m_matrix[nfilas][ncolumnas];
}

matrix matrix::operator + (matrix& B){
    matrix sum(num_columnas, num_filas, 0);
    unsigned i,j;
    for (i=0;i<num_columnas;i++)
        for (j=0;j<num_filas;j++)
            sum(i,j)=this->m_matrix[i][j] + B(i,j);

    return sum;
}



matrix matrix::operator+ (int& scalar){
    matrix result(num_filas,num_columnas,0);
    unsigned i,j;
    for (i = 0; i < num_filas; i++)
        for (j = 0; j < num_columnas; j++)
            result(i,j) = this->m_matrix[i][j] + scalar;

    return result;
}

matrix matrix::operator- (matrix& B){
    matrix rest(num_columnas, num_filas, 0);
    unsigned i,j;
    for (i=0;i<num_columnas;i++)
        for (j=0;j<num_filas;j++)
            rest(i,j)=this->m_matrix[i][j] - B(i,j);
    return rest;
}

matrix matrix::operator* (int &scalar){
    matrix res(num_filas,num_columnas,0);
    unsigned i,j;
    for (i=0;i<num_filas;i++)
        for (j=0;j<num_columnas;j++)
            res(i,j)=this->m_matrix[i][j] * scalar;
    return res;
}

void matrix::print() const
{
    cout << "Matrix: " << endl;
    for (unsigned i=0;i<num_filas;i++) {
        for (unsigned j=0;j<num_columnas;j++) {
            cout << "[" << m_matrix[i][j] << "] ";
        }
        cout << endl;
    }
}

matrix::matrix(unsigned filas,unsigned cols,int** list)
{
    num_filas = filas;
    num_columnas = cols;
    m_matrix.resize(filas);
    for(unsigned i=0;i<m_matrix.size();i++)
        m_matrix[i].resize(cols,0);

    for(unsigned x=0;x<m_matrix.size();x++)
        for(unsigned y=0;y<m_matrix.size();y++)
            m_matrix[x][y]=list[x][y];
}

int **alista(int m, int n,int* valores)
{
     int i;
     int **tmp = (int**) malloc(m * sizeof(int *));
     for(i = 0; i < m; i++)
     tmp[i] = (int*) malloc(n * sizeof(int));

     for(int x=0;x<m;x++)
         for(int y=0;y<n;y++)
            tmp[x][y]=valores[y+(x*m)];

    return tmp;
}
//cambiar m y n por getter de filas y columnas
void matrix::print_latex(int m,int n)
{
 ofstream salida("salida.txt");
 salida<<"$"<<endl;
 salida<<"\\begin{pmatrix}"<<endl;
 int aux=0;
   for(int x=0;x<m;x++)
   {
       for(int y=0;y<n-1;y++)
       {
           salida<<this->m_matrix[x][y]<<" & ";
           aux=y;
       }
       salida<<this->m_matrix[x][aux+1];
       aux=0;
       salida<<"\\\\"<<endl;
   }
salida<<"\\end{pmatrix}"<<endl;
salida<<"$";

    return;
}

int main()
{
    /*
    int v1[] = {1,2,3};
    int v2[] = {4,5,6};
    int v3[] = {7,8,9};
    int* lst[]={v1,v2,v3};
    matrix Z = {3,3,lst};
    matrix A = {3,3,1};
    matrix B = {3,3,2};
    Z.print();
    auto j= Z+A;
    j.print();
*/
    int valores[]={1,2,3,
                   4,5,6,
                  7,8,9};
    int** ls2=alista(3,3,valores);

    matrix P = {3,3,ls2};
    auto Q=P+P;
    Q.print();
    Q.print_latex(3,3);

    return 0;
}

