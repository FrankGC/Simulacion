﻿using Simulacion.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WpfMath.Controls;


namespace Simulacion.Pages
{
    /// <summary>
    /// Interaction logic for Inicio.xaml
    /// </summary>
    public partial class Inicio : Page
    {
        public Inicio()
        {
            InitializeComponent();
            GenerarListas();
            GenerarColumnas();
            sl_main.Value = 10;
            for (int i = 1; i <= 30; i++)
                cbx_nRangos.Items.Add(i);

            cbx_nRangos.SelectedIndex = 3;

        }
            List<DemandaMensualRow> lista_demandamensual;
            List<FactorRow> lista_factores;
            List<TiempoDeEntregraRow> lista_tiempoEntrega;
            List<Procedimiento> lista_procedimiento;
            List<Resultados> lista_resultados;
            List<NumeroAleatorio> lista_numeros;

            private double A;
            private double Xn;
            private double C;
            private double M;
            double SumDemanda = 0;

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
                lista_numeros = new List<NumeroAleatorio>();
                dg_numeros.ItemsSource = lista_numeros;
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

                //Rangos
                DataGridTextColumn nRango = new DataGridTextColumn();
                nRango.Header = "N";
                nRango.Binding = new Binding("posicion");
                dg_rangos.Columns.Add(nRango);
                DataGridTextColumn lim_inf = new DataGridTextColumn();
                lim_inf.Header = "Limite Inferior";
                lim_inf.Binding = new Binding("LimiteInferior");
                dg_rangos.Columns.Add(lim_inf);
                DataGridTextColumn lim_sup = new DataGridTextColumn();
                lim_sup.Header = "Limite Superior";
                lim_sup.Binding = new Binding("LimiteSuperior");
                dg_rangos.Columns.Add(lim_sup);
                DataGridTextColumn frecuenciaEsperada = new DataGridTextColumn();
                frecuenciaEsperada.Header = "Frecuencia esperada";
                frecuenciaEsperada.Binding = new Binding("FrecuenciaEsperada");
                dg_rangos.Columns.Add(frecuenciaEsperada);
                DataGridTextColumn frecuenciaObenida = new DataGridTextColumn();
                frecuenciaObenida.Header = "Frecuencia obtenida";
                frecuenciaObenida.Binding = new Binding("FrecuenciaObtenida");              
                dg_rangos.Columns.Add(frecuenciaObenida);

            }

            private void CargarSemillaTiempoEntrega()
            {
                lista_tiempoEntrega.Add(new TiempoDeEntregraRow { mes = 1.ToString(), probabilidad = 0.ToString(), lim_inicial = 0.ToString(), lim_final = 0.ToString() });
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
                    && tbx_decimales.BorderBrush != Brushes.Red)
                    return true;
                else
                    return false;
            }

            private bool ValidarCampo(TextBox tbox)
            {
                if (tbox.Text != "")
                {
                    try
                    {
                        float.Parse(tbox.Text);
                        tbox.BorderBrush = Brushes.LimeGreen;
                        return true;
                    }
                    catch { tbox.BorderBrush = Brushes.Red; return false; }                  
                }
            return false;
        }
            private void GenerarNumeros()
            {
                int decimales = int.Parse(tbx_decimales.Text);
                A = Math.Round(double.Parse(tbx_A.Text), decimales);
                Xn = Math.Round(double.Parse(tbx_Xn.Text), decimales);
                C = Math.Round(double.Parse(tbx_C.Text), decimales);
                M = Math.Round(double.Parse(tbx_M.Text), decimales);
                lista_numeros.Clear();
                double Aleatorio;
                for (int i = 0; i < double.Parse(tbx_interaciones.Text); i++)
                {
                    Xn = ((A * Xn) + C) % M;
                    if (Xn > 0)
                        Aleatorio = Math.Round(Aleatorio = Xn / M, decimales);
                    else
                        Aleatorio = 0;
                    lista_numeros.Add(new NumeroAleatorio { no = (i + 1).ToString(), numero = Aleatorio.ToString() });
                    
                }
                dg_numeros.Items.Refresh();
                RealizarPruebaDeFrecuencia();
            }

            private void tbx_interaciones_TextChanged(object sender, TextChangedEventArgs e)
            {
                ValidarCampo((TextBox)sender);
                if (NotNulls())
                    GenerarNumeros();
            }

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
                if (ValidadProbabilidadDemanda() == true)
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
                if (ValidarCamposProcedimiento())
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

                    int n_orden = 0;
                    int tiempoOrden = 0;
                    int contTemp = 0;

                    for (int i = 0; i < 12; i++)
                    {

                        if (tiempoOrden != 0)
                        {
                            contTemp++;
                            if (contTemp == tiempoOrden)
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
                        demandaAjustada = Math.Round(double.Parse(lista_factores[i].factor) * demandaMensual);
                        Inv_final = Inv_inicial - demandaAjustada;
                        if (Inv_final <= 0)
                        {
                            falt = Inv_final * -1;
                            Inv_final = 0;
                        }
                        inv_promedio = Math.Round(inventarioPromedio(Inv_inicial, Inv_final, demandaAjustada), 2);
                        if (Inv_final < Q && tiempoOrden <= 0)
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
                                mes = (i + 1).ToString(),
                                inv_inicial = Inv_inicial.ToString(),
                                num = numero.ToString(),
                                dem_ajustada = demandaAjustada.ToString(),
                                inv_final = Inv_final.ToString(),
                                faltante = falt.ToString(),
                                orden = n_orden.ToString(),
                                inv_mensual = inv_promedio.ToString()

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
                double sum = 0;
                foreach (Procedimiento fila in lista_procedimiento)
                {
                    if (fila.inv_mensual != "")
                        sum += double.Parse(fila.inv_mensual);

                }
                sum *= 20;
                costoTotal = costofaltante + costodeorden + costoinventario;
                lista_resultados.Add(new Resultados
                {
                    no = (lista_resultados.Count + 1).ToString(),
                    R = tbx_Q.Text,
                    Q = tbx_R.Text,
                    Cost_orden = costodeorden.ToString(),
                    Cost_faltante = costofaltante.ToString()
                ,
                    Cost_inv = sum.ToString(),
                    Cost_total = costoTotal.ToString()
                });
                dg_resulatados.Items.Refresh();
            }

            private double inventarioPromedio(double inv_inicial, double inv_final, double demandaAjustada)
            {
                if (inv_final <= 0)
                    return (inv_inicial / 2) * (inv_inicial / demandaAjustada);

                else
                    return (inv_final + inv_inicial) / 2;

            }

            private bool ValidarCamposProcedimiento()
            {
                if (tbx_invInicial.Text != "" && tbx_Q.Text != "" && tbx_R.Text != "")
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
                if (lista_demandamensual.Count > 2)
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

                lista_factores.Add(new FactorRow { mes = 1.ToString(), factor = 1.2.ToString() });
                lista_factores.Add(new FactorRow { mes = 2.ToString(), factor = 1.ToString() });
                lista_factores.Add(new FactorRow { mes = 3.ToString(), factor = 0.9.ToString() });
                lista_factores.Add(new FactorRow { mes = 4.ToString(), factor = 0.8.ToString() });
                lista_factores.Add(new FactorRow { mes = 5.ToString(), factor = 0.8.ToString() });
                lista_factores.Add(new FactorRow { mes = 6.ToString(), factor = 0.7.ToString() });
                lista_factores.Add(new FactorRow { mes = 7.ToString(), factor = 0.8.ToString() });
                lista_factores.Add(new FactorRow { mes = 8.ToString(), factor = 0.9.ToString() });
                lista_factores.Add(new FactorRow { mes = 9.ToString(), factor = 1.ToString() });
                lista_factores.Add(new FactorRow { mes = 10.ToString(), factor = 1.2.ToString() });
                lista_factores.Add(new FactorRow { mes = 11.ToString(), factor = 1.3.ToString() });
                lista_factores.Add(new FactorRow { mes = 12.ToString(), factor = 1.4.ToString() });

                lista_tiempoEntrega.Add(new TiempoDeEntregraRow { mes = 1.ToString(), probabilidad = 0.30.ToString() });
                lista_tiempoEntrega.Add(new TiempoDeEntregraRow { mes = 2.ToString(), probabilidad = 0.40.ToString() });
                lista_tiempoEntrega.Add(new TiempoDeEntregraRow { mes = 3.ToString(), probabilidad = 0.30.ToString() });

                
                tbx_Q.Text = "100";
                tbx_R.Text = "120";
                tbx_invInicial.Text = "200";
                dg_demandamensual.Items.Refresh();
                dg_factores.Items.Refresh();
                dg_tiempoEntrega.Items.Refresh();
                dg_procedimiento.Items.Refresh();
                dg_numeros.Items.Refresh();
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
                if (tbx_invInicial.Text != "" && tbx_Q.Text != "" && tbx_R.Text != "")
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

        private void btn_ajuestes_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.navegador.Navigate(new Ajustes());
        }

        private void RealizarPruebaDeFrecuencia()
        {
            if (lista_numeros.Count > 0)
            {
                wrp_frecuencia.Children.Clear();
                int NumeroDeRangos = int.Parse(cbx_nRangos.SelectedValue.ToString());
                PruebaFrecuencia P1 = new PruebaFrecuencia(lista_numeros, NumeroDeRangos);
                List<Rango> listaRangos = P1.CalcularRangos();
                dg_rangos.ItemsSource = listaRangos;
                dg_rangos.Items.Refresh();
                double xo = 0;
                foreach (Rango rango in listaRangos)
                {
                    if (rango.posicion == 1)
                        wrp_frecuencia.Children.Add(new FormulaControl() { Formula = @"= \frac{(" + rango.FrecuenciaObtenida.ToString() + " - " + rango.FrecuenciaEsperada.ToString() + ")^2}{" + rango.FrecuenciaEsperada.ToString() + "}", Margin = new Thickness(5) });
                    else
                        wrp_frecuencia.Children.Add(new FormulaControl() { Formula = @" + \frac{(" + rango.FrecuenciaObtenida.ToString() + " - " + rango.FrecuenciaEsperada.ToString() + ")^2}{" + rango.FrecuenciaEsperada.ToString() + "}", Margin = new Thickness(5) });
                    xo += Math.Pow(rango.FrecuenciaObtenida - rango.FrecuenciaEsperada, 2) / rango.FrecuenciaEsperada;
                }
                if(listaRangos.Count>0)
                ((FormulaControl)wrp_frecuencia.Children[wrp_frecuencia.Children.Count - 1]).Formula += " = " + Math.Round(xo, 2);

                if(stkp_frecuencia.Children.Count>9) 
                    stkp_frecuencia.Children.RemoveRange(9, stkp_frecuencia.Children.Count-9);

                    stkp_frecuencia.Children.Add(new FormulaControl() { Formula = " X_{0}^2 = " + Math.Round(xo, 2), Margin = new Thickness(5), HorizontalContentAlignment=HorizontalAlignment.Center });
                if (cbx_alpha.SelectedIndex >-1)
                {
                    try
                    {
                        double x2 = Math.Round(double.Parse(obtenerChicuadrado(int.Parse(cbx_nRangos.SelectedValue.ToString()) - 1, cbx_alpha.SelectedIndex)), 4);
                        stkp_frecuencia.Children.Add(new FormulaControl() { Formula = " X^2(" + cbx_alpha.SelectedValue.ToString() + "," + (int.Parse(cbx_nRangos.SelectedValue.ToString()) - 1) + ") = " + x2, Margin = new Thickness(5), HorizontalContentAlignment = HorizontalAlignment.Center });
                        if (xo <= x2)
                        {
                            stkp_frecuencia.Children.Add(new FormulaControl() { Formula = "X_{0}^2 <= X^2", HorizontalContentAlignment = HorizontalAlignment.Center });
                            stkp_frecuencia.Children.Add(new Viewbox() { Child = new TextBlock() { Text = "Los numeros estan distribuidos uniformemente",Foreground= Brushes.DarkGreen }, Margin = new Thickness(20) });
                        }                          
                        else
                        {
                            stkp_frecuencia.Children.Add(new FormulaControl() { Formula = "X_{0}^2 >= X^2", HorizontalContentAlignment = HorizontalAlignment.Center });
                            stkp_frecuencia.Children.Add(new Viewbox() { Child = new TextBlock() { Text = "Los numeros NO estan distribuidos uniformemente" ,Foreground = Brushes.DarkRed } ,Margin=new Thickness(20)});
                        }
                           

                        
                        } catch (Exception e){ MessageBox.Show(e.ToString()); }              
                   
                }

                
                    
            }
        }

        private string obtenerChicuadrado(int GradosDeLibertad,int Probabilidad)
        {
            string[][] result =
           (@"df  0.995	  0.99	 0.975	 0.95	 0.90	 0.10	 0.05	 0.025	 0.01	 0.005 \n" +
            "1      0        0   0.001   0.004   0.016   2.706   3.841   5.024   6.635   7.879 \n" +
            "2   0.010   0.020   0.051   0.103   0.211   4.605   5.991   7.378   9.210   10.597 \n" +
            "3   0.072   0.115   0.216   0.352   0.584   6.251   7.815   9.348   11.345  12.838 \n" +
            "4   0.207   0.297   0.484   0.711   1.064   7.779   9.488   11.143  13.277  14.860 \n" +
            "5   0.412   0.554   0.831   1.145   1.610   9.236   11.070  12.833  15.086  16.750 \n" +
            "6   0.676   0.872   1.237   1.635   2.204   10.645  12.592  14.449  16.812  18.548 \n" +
            "7   0.989   1.239   1.690   2.167   2.833   12.017  14.067  16.013  18.475  20.278 \n" +
            "8   1.344   1.646   2.180   2.733   3.490   13.362  15.507  17.535  20.090  21.955 \n" +
            "9   1.735   2.088   2.700   3.325   4.168   14.684  16.919  19.023  21.666  23.589 \n" +
            "10  2.156   2.558   3.247   3.940   4.865   15.987  18.307  20.483  23.209  25.188 \n" +
            "11  2.603   3.053   3.816   4.575   5.578   17.275  19.675  21.920  24.725  26.757 \n" +
            "12  3.074   3.571   4.404   5.226   6.304   18.549  21.026  23.337  26.217  28.300 \n" +
            "13  3.565   4.107   5.009   5.892   7.042   19.812  22.362  24.736  27.688  29.819 \n" +
            "14  4.075   4.660   5.629   6.571   7.790   21.064  23.685  26.119  29.141  31.319 \n" +
            "15  4.601   5.229   6.262   7.261   8.547   22.307  24.996  27.488  30.578  32.801 \n" +
            "16  5.142   5.812   6.908   7.962   9.312   23.542  26.296  28.845  32.000  34.267 \n" +
            "17  5.697   6.408   7.564   8.672   10.085  24.769  27.587  30.191  33.409  35.718 \n" +
            "18  6.265   7.015   8.231   9.390   10.865  25.989  28.869  31.526  34.805  37.156 \n" +
            "19  6.844   7.633   8.907   10.117  11.651  27.204  30.144  32.852  36.191  38.582 \n" +
            "20  7.434   8.260   9.591   10.851  12.443  28.412  31.410  34.170  37.566  39.997 \n" +
            "21  8.034   8.897   10.283  11.591  13.240  29.615  32.671  35.479  38.932  41.401 \n" +
            "22  8.643   9.542   10.982  12.338  14.041  30.813  33.924  36.781  40.289  42.796 \n" +
            "23  9.260   10.196  11.689  13.091  14.848  32.007  35.172  38.076  41.638  44.181 \n" +
            "24  9.886   10.856  12.401  13.848  15.659  33.196  36.415  39.364  42.980  45.559 \n" +
            "25  10.520  11.524  13.120  14.611  16.473  34.382  37.652  40.646  44.314  46.928 \n" +
            "26  11.160  12.198  13.844  15.379  17.292  35.563  38.885  41.923  45.642  48.290 \n" +
            "27  11.808  12.879  14.573  16.151  18.114  36.741  40.113  43.195  46.963  49.645 \n" +
            "28  12.461  13.565  15.308  16.928  18.939  37.916  41.337  44.461  48.278  50.993 \n" +
            "29  13.121  14.256  16.047  17.708  19.768  39.087  42.557  45.722  49.588  52.336 \n" +
            "30  13.787  14.953  16.791  18.493  20.599  40.256  43.773  46.979  50.892  53.672 \n")
            .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();
           
            return result[GradosDeLibertad][Probabilidad].ToString();
        }

        private void sl_main_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            scv_main.ScrollToVerticalOffset(scv_main.ScrollableHeight-(sl_main.Value*(scv_main.ScrollableHeight / 10)) );
            bloque.Text = "Max: "+scv_main.ScrollableHeight.ToString()+ "ACT: "+sl_main.Value.ToString();
        }

        private void cbx_nRangos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                RealizarPruebaDeFrecuencia();
            }
            catch { }
        }

        private void cbx_alpha_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                RealizarPruebaDeFrecuencia();
            }
            catch { }
        }
    }
}
