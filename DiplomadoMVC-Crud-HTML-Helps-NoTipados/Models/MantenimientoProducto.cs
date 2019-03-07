using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace DiplomadoMVC_Crud_HTML_Helps_NoTipados.Models
{
    public class MantenimientoProducto
    {

  
            private SqlConnection con;

            private void Conectar()
            {
                string constr = ConfigurationManager.ConnectionStrings["ConexionDB"].ToString();
                con = new SqlConnection(constr);
            }
            public int Agregar(Productos pro)
            {
                Conectar();
                SqlCommand comando = new SqlCommand("insert into productos(Descripcion, Precio) values (@Descripcion,@Precio)", con);
                comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
                comando.Parameters.Add("@Precio", SqlDbType.Float);
                comando.Parameters["@Descripcion"].Value = pro.Descripcion;
                comando.Parameters["@Precio"].Value = pro.Precio;
                con.Open();
                int i = comando.ExecuteNonQuery();
                con.Close();
                return i;
            }
            public List<Productos> RecuperarTodos()
            {
                Conectar();
                List<Productos> productos = new List<Productos>();
                SqlCommand com = new SqlCommand("select Codigo,descripcion,Precio from Productos", con);
                con.Open();
                SqlDataReader registros = com.ExecuteReader();
                while (registros.Read())
                {
                    Productos pro = new Productos
                    {
                        Codigo = int.Parse(registros["Codigo"].ToString()),
                        Descripcion = registros["Descripcion"].ToString(),
                        Precio = int.Parse(registros["Precio"].ToString())
                    };
                    productos.Add(pro);

                }
                con.Close();
                return productos;
            }
            public Productos Recuperar(int codigo)
            {
                Conectar();
                SqlCommand comando = new SqlCommand("select Codigo,Descripcion,Precio from Productos where Codigo=@Codigo", con);
                comando.Parameters.Add("@Codigo", SqlDbType.Int);
                comando.Parameters["@Codigo"].Value = codigo;
                con.Open();
                SqlDataReader registros = comando.ExecuteReader();
                Productos producto = new Productos();
                if (registros.Read())
                {
                    producto.Codigo = int.Parse(registros["Codigo"].ToString());
                    producto.Descripcion = registros["Descripcion"].ToString();
                    producto.Precio = float.Parse(registros["Precio"].ToString());
                }
                else
                    producto = null;
                con.Close();
                return producto;
            }
            public int Modificar(Productos pro)
            {
                Conectar();
                SqlCommand comando = new SqlCommand("update Productos set Descripcion=@Descripcion,Precio=@Precio where Codigo=@Codigo", con);
                comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
                comando.Parameters["@Descripcion"].Value = pro.Descripcion;
                comando.Parameters.Add("@Precio", SqlDbType.Float);
                comando.Parameters["@Precio"].Value = pro.Precio;
                comando.Parameters.Add("@Codigo", SqlDbType.Int);
                comando.Parameters["@Codigo"].Value = pro.Codigo;
                con.Open();
                int i = comando.ExecuteNonQuery();
                con.Close();
                return i;
            }
            public int Borrar(int Codigo)
            {
                Conectar();
                SqlCommand comando = new SqlCommand("delete from Productos where Codigo=@Codigo", con);
                comando.Parameters.Add("@Codigo", SqlDbType.Int);
                comando.Parameters["@Codigo"].Value = Codigo;
                con.Open();
                int i = comando.ExecuteNonQuery();
                con.Close();
                return i;
            }
        }

    }
