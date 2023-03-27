using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prySP1Autocor
{
    public partial class frmRepuestos : Form
    {
        // definimos en nombre del archivo en una constante
        private const string PATH_ARCHIVO = "Repuestos.txt";

        public frmRepuestos()
        {
            InitializeComponent();
        }

        private void mrcRepuesto_Enter(object sender, EventArgs e)
        {

        }

        private void frmRepuestos_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            txtCodigo.Text = ""; // limpiar los textBox
            txtNombre.Text = "";
            txtPrecio.Text = "";
            // cargar el comboBox
            cmbMarca.Items.Clear();
            cmbMarca.Items.Add("Marca A");
            cmbMarca.Items.Add("Marca B");
            cmbMarca.Items.Add("Marca C");
            cmbMarca.SelectedIndex = 0;
            // marcar la opción de origen Nacional
            optNacional.Checked = true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos()) // si los datos son correctos
            {
                // crear un nuevo repuesto 
                Repuesto nuevoRep = CrearRepuesto();
                // grabar en el archivo
                Archivo Repuestos = new Archivo();
                Repuestos.NombreArchivo = PATH_ARCHIVO;
                Repuestos.GrabarRepuesto(nuevoRep);
                // restaurar la interfaz al estado inicial
                Inicializar();
            }
            else // si hay algún error
            {
                MessageBox.Show("Datos incorrectos", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Repuesto CrearRepuesto()
        {
            // se crea un nuevo objeto de tipo Repuesto
            Repuesto nuevoRep = new Repuesto();
            // se asignan los valores a todas sus propiedades
            nuevoRep.Codigo = txtCodigo.Text;
            nuevoRep.Nombre = txtNombre.Text;
            nuevoRep.Marca = cmbMarca.SelectedItem.ToString();
            nuevoRep.Precio = decimal.Parse(txtPrecio.Text);
            if (optNacional.Checked)
            {
                nuevoRep.Origen = "Nacional";
            }
            else
            {
                nuevoRep.Origen = "Importado";
            }
            return nuevoRep; // devuelve el objeto creado con sus valores
        }

        private bool ValidarDatos()
        {
            // devuelve falso si no se cumplen todas las condiciones
            bool resultado = false;
            if (txtCodigo.Text != "") // controla el valor del código
            {
                if (txtNombre.Text != "") // controla el nombre
                {
                    if (txtPrecio.Text != "") // controla el precio
                    {
                        Archivo Repuestos = new Archivo();
                        Repuestos.NombreArchivo = PATH_ARCHIVO;
                        // controla que no se repita el código del repuesto
                        if (Repuestos.BuscarCodigoRepuesto(txtCodigo.Text) ==
                        false)
                        {
                            resultado = true; // devuelve verdadero sólo si todas
                                              // las condiciones se cumplieron
                        }
                    }
                }
            }
            return resultado;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            // se crea un objeto de tipo frmConsulta pasando el path al archivo de repuestos
            frmConsulta frm = new frmConsulta(PATH_ARCHIVO);
            frm.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        // este evento nos garantiza que txtPrecio contendrá siempre
        // un valor numérico de tipo decimal o estará
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // aceptar solo expresiones numéricas con decimales
            if (!Char.IsNumber(e.KeyChar) &&
            e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            if (e.KeyChar == ',' && txtPrecio.Text.Contains(","))
            {
                e.Handled = true;
            }
        }
    }
}
