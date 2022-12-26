using System;
using System.Collections.Generic;
using Gtk;

namespace dio_gtksharp_banktransfer
{
    public enum TipoConta
    {
        PessoaFisica = 1,
        PessoaJuridica = 2
    }

    public enum SaqueRes
    {
        OK = 1,
        SemCred = 2
    }

    public static class dados {
        private static int codctd = 0;
        public static List<Conta> Ctalist = new List<Conta>(); 
        public static Gtk.ListStore modelStore = new Gtk.ListStore(typeof (int),
                                                        typeof (string),
                                                        typeof (string),
                                                        typeof (double),
                                                        typeof (double));
            

        public static void CadastraConta(Conta ct) {
            ct.Cod = codctd+=1;
            Ctalist.Add(ct);
            modelStore.AppendValues(ct.Cod, ct.Nome, (ct.TipoConta == TipoConta.PessoaFisica ? "F" : "J"), ct.Saldo, ct.Credito);            
        }

        public static void updateStore(int cod, int col, object vlr) {
            dados.modelStore.Foreach(new TreeModelForeachFunc((ITreeModel model, TreePath path, TreeIter iter) => {
                if ((int)dados.modelStore.GetValue(iter, 0) == cod) {
                    dados.modelStore.SetValue(iter,col, vlr);
                    return true;
                }
                return false;
            }));
        }

        public static Conta GetConta(int cod) {
            return Ctalist.Find(c => c.Cod == cod);
        }
    }

    public class Conta {

        public int Cod { get; set; }
        public string Nome { get; set; }
        public TipoConta TipoConta { get; set; }
		public double Saldo { get; set; }
		public double Credito { get; set; }

        public Conta(string Nome, TipoConta TipoConta, double Saldo, double Credito) {
            this.Nome = Nome;
            this.TipoConta = TipoConta;
            this.Saldo = Saldo;
            this.Credito = Credito;
        }
		
        public void Depositar(double valor) {
            this.Saldo += valor;
        }

        public SaqueRes Sacar(double valor) {
            if (this.Saldo-valor < this.Credito*-1) return SaqueRes.SemCred;
            this.Saldo -= valor;
            return SaqueRes.OK;         
        }

        public SaqueRes Transferir(double valor, Conta dest) {
            SaqueRes res = Sacar(valor);
            if (res == SaqueRes.OK) dest.Depositar(valor);
            return res;            
        }
    };

}