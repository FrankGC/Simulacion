using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Data;

namespace Simulacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        List<DemandaMensualRow> lista_demandamensual;
        List<FactorRow> lista_factores;
        List<TiempoDeEntregraRow> lista_tiempoEntrega;
        List<Procedimiento> lista_procedimiento;
        List<Resultados> lista_resultados;

        private double A;
        private double Xn;
        private double C;
        private double M;
        double SumDemanda =0;

        public string var_xn { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            WindowTitleBrush = Brushes.Teal;
            
            GenerarListas();
            GenerarColumnas();
            
        }
        private void GenerarListas()
        {
            lista_demandamensual = new List<DemandaMensualRow>();
            dg_demandamensual.ItemsSource = lista_demandamensual;
            lista_factores = new List<FactorRow>();
            dg_factores.ItemsSource = lista_factores;
            lista_tiempoEntrega = new List<TiempoDeEntregraRow>();
            dg_tiempoEntrega.ItemsSource = lista_tiempoEntrega;
            lista_procedimiento = new List<Procedimiento>();
            dg_procedimiento.ItemsSource = lista_procedimiento;
            lista_resultados = new List<Resultados>();
            dg_resulatados.ItemsSource = lista_resultados;
        }

        private void GenerarColumnas()
        {
            //Numero aleatorios
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "No";
            c1.Binding = new Binding("no");
            c1.IsReadOnly = true;
            dg_numeros.Columns.Add(c1);
            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Numero";
            c2.Binding = new Binding("numero");
            c2.IsReadOnly = true;
            dg_numeros.Columns.Add(c2);

            //Demanda mensual
            DataGridTextColumn cant = new DataGridTextColumn();
            cant.Header = "Cant.";
            cant.Binding = new Binding("cantidad");
            dg_demandamensual.Columns.Add(cant);
            DataGridTextColumn prob = new DataGridTextColumn();
            prob.Header = "Prob.";
            prob.Binding = new Binding("probabilidad");
            dg_demandamensual.Columns.Add(prob);
            DataGridTextColumn lim_inicial = new DataGridTextColumn();
            lim_inicial.Header = "Lim inicial";
            lim_inicial.Binding = new Binding("lim_inicial");
            lim_inicial.IsReadOnly = true;
            dg_demandamensual.Columns.Add(lim_inicial);
            DataGridTextColumn lim_final = new DataGridTextColumn();
            lim_final.Header = "Lim final";
            lim_final.Binding = new Binding("lim_final");
            lim_final.IsReadOnly = true;
            dg_demandamensual.Columns.Add(lim_final);

            //Factores estacionales
            DataGridTextColumn mes = new DataGridTextColumn();
            mes.Header = "Mes";
            mes.Binding = new Binding("mes");
            mes.IsReadOnly = true;
            dg_factores.Columns.Add(mes);
            DataGridTextColumn factor = new DataGridTextColumn();
            factor.Header = "Factor";
            factor.Binding = new Binding("factor");
            dg_factores.Columns.Add(factor);
            CargarSemillaFactores();

            //Tiempo de entrega
            DataGridTextColumn meses = new DataGridTextColumn();
            meses.Header = "Meses";
            meses.Binding = new Binding("mes");
            dg_tiempoEntrega.Columns.Add(meses);
            DataGridTextColumn probabilidad = new DataGridTextColumn();
            probabilidad.Header = "Pro. ";
            probabilidad.Binding = new Binding("probabilidad");
            dg_tiempoEntrega.Columns.Add(probabilidad);
            DataGridTextColumn inicio = new DataGridTextColumn();
            inicio.Header = "Lim inicial";
            inicio.Binding = new Binding("lim_inicial");
            dg_tiempoEntrega.Columns.Add(inicio);
            DataGridTextColumn fin = new DataGridTextColumn();
            fin.Header = "Lim final";
            fin.Binding = new Binding("lim_final");
            dg_tiempoEntrega.Columns.Add(fin);
            dg_tiempoEntrega.CanUserAddRows = false;
            CargarSemillaTiempoEntrega();

            //Procedimiento
            DataGridTextColumn Mes1 = new DataGridTextColumn();
            Mes1.Header = "Mes";
            Mes1.Binding = new Binding("mes");
            dg_procedimiento.Columns.Add(Mes1);
            DataGridTextColumn inv_i = new DataGridTextColumn();
            inv_i.Header = "Inv. Inicial";
            inv_i.Binding = new Binding("inv_inicial");
            dg_procedimiento.Columns.Add(inv_i);
            DataGridTextColumn num = new DataGridTextColumn();
            num.Header = "Num. Pseudo";
            num.Binding = new Binding("num");
            dg_procedimiento.Columns.Add(num);
            DataGridTextColumn dem_ajustada = new DataGridTextColumn();
            dem_ajustada.Header = "Dem. Ajustada";
            dem_ajustada.Binding = new Binding("dem_ajustada");
            dg_procedimiento.Columns.Add(dem_ajustada);
            DataGridTextColumn inv_final1 = new DataGridTextColumn();
            inv_final1.Header = "Inv. Final";
            inv_final1.Binding = new Binding("inv_final");
            dg_procedimiento.Columns.Add(inv_final1);
            DataGridTextColumn faltante = new DataGridTextColumn();
            faltante.Header = "Faltante";
            faltante.Binding = new Binding("faltante");
            dg_procedimiento.Columns.Add(faltante);
            DataGridTextColumn Orden = new DataGridTextColumn();
            Orden.Header = "Orden";
            Orden.Binding = new Binding("orden");
            dg_procedimiento.Columns.Add(Orden);
            DataGridTextColumn inv_mensual = new DataGridTextColumn();
            inv_mensual.Header = "Inv. Mensual";
            inv_mensual.Binding = new Binding("inv_mensual");
            dg_procedimiento.Columns.Add(inv_mensual);

            //Resultados
            DataGridTextColumn no = new DataGridTextColumn();
            no.Header = "No.";
            no.Binding = new Binding("no");
            dg_resulatados.Columns.Add(no);
            DataGridTextColumn R = new DataGridTextColumn();
            R.Header = "R";
            R.Binding = new Binding("R");
            dg_resulatados.Columns.Add(R);
            DataGridTextColumn Q = new DataGridTextColumn();
            Q.Header = "Q";
            Q.Binding = new Binding("Q");
            dg_resulatados.Columns.Add(Q);
            DataGridTextColumn costo_orden = new DataGridTextColumn();
            costo_orden.Header = "Costo de orden";
            costo_orden.Binding = new Binding("Cost_orden");
            dg_resulatados.Columns.Add(costo_orden);
            DataGridTextColumn costo_faltante = new DataGridTextColumn();
            costo_faltante.Header = "Costo de faltante";
            costo_faltante.Binding = new Binding("Cost_faltante");
            dg_resulatados.Columns.Add(costo_faltante);
            DataGridTextColumn costo_inv = new DataGridTextColumn();
            costo_inv.Header = "Consto de inventario";
            costo_inv.Binding = new Binding("Cost_inv");
            dg_resulatados.Columns.Add(costo_inv);
            DataGridTextColumn costo_total = new DataGridTextColumn();
            costo_total.Header = "Costo de total";
            costo_total.Binding = new Binding("Cost_total");
            dg_resulatados.Columns.Add(costo_total);
            DataGridTextColumn mejorOpcion = new DataGridTextColumn();
            mejorOpcion.Header = "Mejor Opcion";
            mejorOpcion.Binding = new Binding("mejorOpcion");
            dg_resulatados.Columns.Add(mejorOpcion);

        }

        private void CargarSemillaTiempoEntrega()
        {
            lista_tiempoEntrega.Add(new TiempoDeEntregraRow { mes = 1.ToString(), probabilidad = 0.ToString(), lim_inicial = 0.ToString(),lim_final=0.ToString()});
            lista_tiempoEntrega.Add(new TiempoDeEntregraRow { mes = 2.ToString(), probabilidad = 0.ToString(), lim_inicial = 0.ToString(), lim_final = 0.ToString() });
            lista_tiempoEntrega.Add(new TiempoDeEntregraRow { mes = 3.ToString(), probabilidad = 0.ToString(), lim_inicial = 0.ToString(), lim_final = 0.ToString() });
        }

        private void CargarSemillaFactores()
        {
            lista_factores.Add(new FactorRow { mes = 1.ToString(), factor = 0.ToString() });
            lista_factores.Add(new FactorRow { mes = 2.ToString(), factor = 0.ToString() });
            lista_factores.Add(new FactorRow { mes = 3.ToString(), factor = 0.ToString() });
            lista_factores.Add(new FactorRow { mes = 4.ToString(), factor = 0.ToString() });
            lista_factores.Add(new FactorRow { mes = 5.ToString(), factor = 0.ToString() });
            lista_factores.Add(new FactorRow { mes = 6.ToString(), factor = 0.ToString() });
            lista_factores.Add(new FactorRow { mes = 7.ToString(), factor = 0.ToString() });
            lista_factores.Add(new FactorRow { mes = 8.ToString(), factor = 0.ToString() });
            lista_factores.Add(new FactorRow { mes = 9.ToString(), factor = 0.ToString() });
            lista_factores.Add(new FactorRow { mes = 10.ToString(), factor = 0.ToString() });
            lista_factores.Add(new FactorRow { mes = 11.ToString(), factor = 0.ToString() });
            lista_factores.Add(new FactorRow { mes = 12.ToString(), factor = 0.ToString() });

        }

        private bool NotNulls()
        {
            if (tbx_interaciones.Text != "" 
                && tbx_A.Text != "" 
                && tbx_Xn.Text != "" 
                && tbx_C.Text != ""
                && tbx_M.Text != ""
                && tbx_decimales.Text != ""
                && tbx_interaciones.BorderBrush != Brushes.Red
                && tbx_A.BorderBrush != Brushes.Red
                && tbx_Xn.BorderBrush != Brushes.Red
                && tbx_C.BorderBrush != Brushes.Red
                && tbx_M.BorderBrush != Brushes.Red
                && tbx_decimales.BorderBrush!= Brushes.Red)
                return true;
            else
                return false;
        }

        private void ValidarCampo(TextBox tbox)
        {
            if (tbox.Text != "")
            {
                try
                {
                    float.Parse(tbox.Text);
                    tbox.BorderBrush = Brushes.LimeGreen;
                }
                catch { tbox.BorderBrush = Brushes.Red; }
            }
        }
        private void GenerarNumeros()
        {
            int decimales = int.Parse(tbx_decimales.Text);
            A = Math.Round(double.Parse(tbx_A.Text), decimales);
            Xn = Math.Round(double.Parse(tbx_Xn.Text), decimales);
            var_xn = Xn.ToString();
            C = Math.Round(double.Parse(tbx_C.Text), decimales);
            M = Math.Round(double.Parse(tbx_M.Text), decimales);
            dg_numeros.Items.Clear();
            double Aleatorio;
            for (int i = 0; i < double.Parse(tbx_interaciones.Text); i++)
            {
                Xn = ((A * Xn) + C) % M;
                if (Xn > 0)
                  Aleatorio = Math.Round( Aleatorio = Xn / M,decimales);
                else
                    Aleatorio = 0;
                dg_numeros.Items.Add(new NumeroAleatorio { no=(i+1).ToString(),numero= Aleatorio.ToString()});
            }
        }

        private void tbx_interaciones_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender);
            if (NotNulls())
                GenerarNumeros();       }

        private void tbx_A_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender);
            if (NotNulls())
                GenerarNumeros();
        }

        private void tbx_Xn_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender);
            if (NotNulls())
                GenerarNumeros();
        }

        private void tbx_C_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender);
            if (NotNulls())
                GenerarNumeros();
        }

        private void tbx_M_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender);
            if (NotNulls())
                GenerarNumeros();
        }

        private void dg_demandamensual_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            TextBox celda = (TextBox)e.EditingElement;
            if (celda.Text == "")
                celda.Text = 0.ToString();
            try
            {
                double.Parse(celda.Text);
            }
            catch { celda.Text = 0.ToString(); }
        }

        private void dg_demandamensual_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ValidadProbabilidadDemanda()==true)
            {
                ProbabilidadAcumuladaDemanda();
                Procedimiento();
            }

        }

        private bool ValidadProbabilidadDemanda()
        {
            bool nulls = false;
            foreach (var fila in dg_demandamensual.Items)
            {
                if (fila.GetType() == typeof(DemandaMensualRow))
                {
                    if (((DemandaMensualRow)fila).probabilidad == null)
                    {
                        nulls = true;
                        SumDemanda = 0;
                        return false;
                    }
                }
            }
            if (nulls == false)
            {
                for (int i = 0; i < dg_demandamensual.Items.Count - 1; i++)
                {
                    if (dg_demandamensual.Items[i].GetType() == typeof(DemandaMensualRow))
                        SumDemanda += double.Parse(((DemandaMensualRow)dg_demandamensual.Items[i]).probabilidad);
                }
               
            }
            return true;
        }

        private double Pseudo(int n)
        {
            return double.Parse(((NumeroAleatorio)dg_numeros.Items[n]).numero);
        }

        private void Procedimiento()
        {         
            if(ValidarCamposProcedimiento())
            {
                lista_procedimiento.Clear();
                int n_aleatorio = 0;
                double Inv_inicial = double.Parse(tbx_invInicial.Text);
                double Inv_final = 0;
                double Q = double.Parse(tbx_Q.Text);
                double R = double.Parse(tbx_R.Text);
                double demandaMensual = 0;
                double demandaAjustada = 0;
                double numero = 0;
                double inv_promedio = 0;
                double falt = 0;
                 
                int n_orden =0;
                int tiempoOrden=0;
                int contTemp = 0;

                for (int i = 0; i < 12; i++)
                {

                    if(tiempoOrden!=0)
                    {
                        contTemp++;
                        if (contTemp==tiempoOrden)
                        {
                            Inv_inicial += Q - falt;
                            tiempoOrden = 0;
                            contTemp = 0;
                            falt = 0;
                        }
                        

                    }
                    numero = Pseudo(n_aleatorio);
                    //Buscar Demanda
                    foreach (DemandaMensualRow fila in lista_demandamensual)
                    {
                        if (double.Parse(fila.lim_inicial) < numero && numero < double.Parse(fila.lim_final))
                        {
                            demandaMensual = double.Parse(fila.cantidad);
                            n_aleatorio++;
                            break;
                        }
                    }
                    demandaAjustada =Math.Round( double.Parse(lista_factores[i].factor) * demandaMensual);
                    Inv_final = Inv_inicial - demandaAjustada;
                    if (Inv_final <= 0)
                    {
                        falt = Inv_final * -1;
                        Inv_final = 0;
                    }
                    inv_promedio = Math.Round(inventarioPromedio(Inv_inicial, Inv_final, demandaAjustada),2);
                    if (Inv_final < Q && tiempoOrden<=0)
                    {
                        n_orden++;
                        
                        foreach (TiempoDeEntregraRow fila in lista_tiempoEntrega)
                        {
                            if (double.Parse(fila.lim_inicial) < Pseudo(n_aleatorio) && Pseudo(n_aleatorio) < double.Parse(fila.lim_final))
                            {
                                tiempoOrden = int.Parse(fila.mes);
                                n_aleatorio++;
                                break;
                            }
                        }

                        lista_procedimiento.Add(new Procedimiento
                        {
                            mes = (i + 1).ToString(), inv_inicial = Inv_inicial.ToString(), num = numero.ToString(),
                            dem_ajustada = demandaAjustada.ToString(), inv_final = Inv_final.ToString(), faltante = falt.ToString(),
                            orden = n_orden.ToString(), inv_mensual = inv_promedio.ToString()
                            
                        });
                        
                    }
                    else
                    {
                        lista_procedimiento.Add(new Procedimiento
                        {
                            mes = (i + 1).ToString(),
                            inv_inicial = Inv_inicial.ToString(),
                            num = numero.ToString(),
                            dem_ajustada = demandaAjustada.ToString(),
                            inv_final = Inv_final.ToString(),
                            faltante = falt.ToString(),
                            orden = "---",
                            inv_mensual = inv_promedio.ToString()

                        });
                    }

                    Inv_inicial = Inv_final;
                  
                }
                dg_procedimiento.Items.Refresh();
                Resulatados();
            }
        }

        private void Resulatados()
        {
            double costodeorden = 100;
            double costoinventario = 20;
            double costofaltante = 50;
            double costoTotal = 0;
            int temp = 0;
            foreach (Procedimiento fila in lista_procedimiento)
            {
                try
                {
                    if (int.Parse(fila.orden) > temp)
                        temp = int.Parse(fila.orden);
                }
                catch { }
                
            }
            costodeorden = costodeorden * temp;
            temp = 0;
            foreach (Procedimiento fila in lista_procedimiento)
            {
                if (fila.faltante != "0")
                    temp += int.Parse(fila.faltante);

            }
            costofaltante = temp * costofaltante;
            double sum=0;
            foreach (Procedimiento fila in lista_procedimiento)
            {
                if (fila.inv_mensual !="")
                    sum += double.Parse(fila.inv_mensual);

            }
            sum *= 20;
            costoTotal = costofaltante + costodeorden+ costoinventario;
            lista_resultados.Add(new Resultados {no=(lista_resultados.Count+1).ToString(),R=tbx_Q.Text,Q=tbx_R.Text,Cost_orden=costodeorden.ToString(),Cost_faltante=costofaltante.ToString()
            ,Cost_inv=sum.ToString(),Cost_total=costoTotal.ToString()});
            dg_resulatados.Items.Refresh();
        }

        private double inventarioPromedio(double inv_inicial,double inv_final,double demandaAjustada)
        {
            if(inv_final<=0)
                return (inv_inicial / 2) * (inv_inicial / demandaAjustada);

            else
                return (inv_final + inv_inicial) / 2;
            
        }

        private bool ValidarCamposProcedimiento()
        {
            if(tbx_invInicial.Text!=""&&tbx_Q.Text!=""&&tbx_R.Text!="")
            {
                try
                {
                    double.Parse(tbx_invInicial.Text);
                    double.Parse(tbx_Q.Text);
                    double.Parse(tbx_R.Text);
                }
                catch { return false; }
                return true;
            }
            return false;
        }

        private void ProbabilidadAcumuladaDemanda()
        {
            if(lista_demandamensual.Count>2)
            {
                lista_demandamensual[0].lim_inicial = 0.ToString();
                lista_demandamensual[0].lim_final = lista_demandamensual[0].probabilidad;
                for (int i = 1; i < lista_demandamensual.Count - 1; i++)
                {
                    lista_demandamensual[i].lim_inicial = lista_demandamensual[i - 1].lim_final;
                    lista_demandamensual[i].lim_final = (double.Parse(lista_demandamensual[i].lim_inicial) + double.Parse(lista_demandamensual[i].probabilidad)).ToString();
                }
                var cell = dg_demandamensual.CurrentCell;
                dg_demandamensual.Items.Refresh();
                dg_demandamensual.Focus();
                dg_demandamensual.CurrentCell = cell;
                
            }   
        }

        private void dg_factores_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            TextBox celda = (TextBox)e.EditingElement;
            if (celda.Text == "")
                celda.Text = 0.ToString();
            try
            {
                double.Parse(celda.Text);
            }
            catch { celda.Text = 0.ToString(); }
        }

        private void dg_tiempoEntrega_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            TextBox celda = (TextBox)e.EditingElement;
            if (celda.Text == "")
                celda.Text = 0.ToString();
            try
            {
                double.Parse(celda.Text);
            }
            catch { celda.Text = 0.ToString(); }
        }

        private void dg_tiempoEntrega_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void dg_tiempoEntrega_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TiempoDeEntrega();
        }

        private void TiempoDeEntrega()
        {
            lista_tiempoEntrega[0].lim_inicial = 0.ToString();
            lista_tiempoEntrega[0].lim_final = lista_tiempoEntrega[0].probabilidad;
            lista_tiempoEntrega[1].lim_inicial = lista_tiempoEntrega[0].lim_final;
            lista_tiempoEntrega[1].lim_final = (double.Parse(lista_tiempoEntrega[1].probabilidad) + double.Parse(lista_tiempoEntrega[1].lim_inicial)).ToString();
            lista_tiempoEntrega[2].lim_inicial = lista_tiempoEntrega[1].lim_final;
            lista_tiempoEntrega[2].lim_final = (double.Parse(lista_tiempoEntrega[2].probabilidad) + double.Parse(lista_tiempoEntrega[2].lim_inicial)).ToString();
            dg_tiempoEntrega.Items.Refresh();
            dg_tiempoEntrega.Focus();
        }

        private void ejemplo()
        {
            lista_demandamensual.Clear();
            lista_tiempoEntrega.Clear();
            lista_procedimiento.Clear();
            lista_factores.Clear();
            dg_numeros.Items.Clear();

            tbx_A.Text = "101";
            tbx_C.Text = "221";
            tbx_interaciones.Text = "100";
            tbx_Xn.Text = "17";
            tbx_M.Text = "15001";




            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 35.ToString(), probabilidad = 0.010.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 36.ToString(), probabilidad = 0.015.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 37.ToString(), probabilidad = 0.020.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 38.ToString(), probabilidad = 0.020.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 39.ToString(), probabilidad = 0.022.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 40.ToString(), probabilidad = 0.023.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 41.ToString(), probabilidad = 0.025.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 42.ToString(), probabilidad = 0.027.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 43.ToString(), probabilidad = 0.028.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 44.ToString(), probabilidad = 0.029.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 45.ToString(), probabilidad = 0.035.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 46.ToString(), probabilidad = 0.045.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 47.ToString(), probabilidad = 0.060.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 48.ToString(), probabilidad = 0.065.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 49.ToString(), probabilidad = 0.070.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 50.ToString(), probabilidad = 0.080.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 51.ToString(), probabilidad = 0.075.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 52.ToString(), probabilidad = 0.070.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 53.ToString(), probabilidad = 0.065.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 54.ToString(), probabilidad = 0.060.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 55.ToString(), probabilidad = 0.050.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 56.ToString(), probabilidad = 0.040.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 57.ToString(), probabilidad = 0.030.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 58.ToString(), probabilidad = 0.016.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 59.ToString(), probabilidad = 0.015.ToString() });
            lista_demandamensual.Add(new DemandaMensualRow { cantidad = 60.ToString(), probabilidad = 0.005.ToString() });

            lista_factores.Add(new FactorRow { mes= 1.ToString(), factor = 1.2.ToString() });
            lista_factores.Add(new FactorRow { mes = 2.ToString(), factor = 1.ToString() });
            lista_factores.Add(new FactorRow { mes = 3.ToString(), factor = 0.9.ToString() });
            lista_factores.Add(new FactorRow { mes = 4.ToString(), factor = 0.8.ToString() });
            lista_factores.Add(new FactorRow { mes = 5.ToString(), factor = 0.8.ToString() });
            lista_factores.Add(new FactorRow { mes = 6.ToString(),factor = 0.7.ToString() });
            lista_factores.Add(new FactorRow { mes = 7.ToString(),factor = 0.8.ToString() });
            lista_factores.Add(new FactorRow { mes = 8.ToString(),factor = 0.9.ToString() });
            lista_factores.Add(new FactorRow { mes = 9.ToString(),factor = 1.ToString() });
            lista_factores.Add(new FactorRow { mes = 10.ToString(),factor = 1.2.ToString() });
            lista_factores.Add(new FactorRow { mes = 11.ToString(),factor = 1.3.ToString() });
            lista_factores.Add(new FactorRow { mes = 12.ToString(),factor = 1.4.ToString() });

            lista_tiempoEntrega.Add(new TiempoDeEntregraRow { mes = 1.ToString(), probabilidad = 0.30.ToString() });
            lista_tiempoEntrega.Add(new TiempoDeEntregraRow { mes = 2.ToString(),probabilidad = 0.40.ToString() });
            lista_tiempoEntrega.Add(new TiempoDeEntregraRow { mes = 3.ToString(),probabilidad = 0.30.ToString() });

            dg_demandamensual.Items.Refresh();
            dg_factores.Items.Refresh();
            dg_tiempoEntrega.Items.Refresh();
            dg_procedimiento.Items.Refresh();
            tbx_Q.Text = "100";
            tbx_R.Text = "120";
            tbx_invInicial.Text = "200";
        }

        private void btn_ejemplo_Click(object sender, RoutedEventArgs e)
        {
            ejemplo();
            TiempoDeEntrega();
            if (ValidadProbabilidadDemanda() == true)
            {
                ProbabilidadAcumuladaDemanda();
                Procedimiento();
                tbx_invInicial.Text = "";
                tbx_Q.Text = "";
                tbx_R.Text = "";

            }

        }

        private void btn_guardar_Click(object sender, RoutedEventArgs e)
        {
            if(tbx_invInicial.Text!=""&&tbx_Q.Text!=""&&tbx_R.Text!="")
            {
                lista_procedimiento.Clear();
                Procedimiento();
                double mayor = 0;
                mayor = double.Parse(lista_resultados[0].Cost_total);
                foreach (Resultados fila in lista_resultados)
                {
                    fila.mejorOpcion = "";
                    if (double.Parse(fila.Cost_total) < mayor)
                        mayor = double.Parse(fila.Cost_total);
                }
                foreach (Resultados fila in lista_resultados)
                {
                    if (double.Parse(fila.Cost_total) == mayor)
                    {
                        fila.mejorOpcion = "<----------";
                        break;
                    }
                      
                }
                tbx_invInicial.Text = "";
                tbx_Q.Text = "";
                tbx_R.Text = "";

            }
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            ejemplo();
        }

        private void btn_helpNumeros_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_editarNumeros_Click(object sender, RoutedEventArgs e)
        {
            if (dwh_numeros.IsTopDrawerOpen == false)
                dwh_numeros.IsTopDrawerOpen = true;
            else
                dwh_numeros.IsTopDrawerOpen = false;
        }

        private void tbx_decimales_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCampo((TextBox)sender);
            if (NotNulls())
                GenerarNumeros();
        }

        private void btn_promedio_Click(object sender, RoutedEventArgs e)
        {
            if (tran_promedio.SelectedIndex != 1)
            {
                tran_promedio.SelectedIndex = 1;

            }                
        }

        private void btn_frecuencia_Click(object sender, RoutedEventArgs e)
        {
            if (tran_promedio.SelectedIndex != 2)
                tran_promedio.SelectedIndex = 2;
        }
    }
}