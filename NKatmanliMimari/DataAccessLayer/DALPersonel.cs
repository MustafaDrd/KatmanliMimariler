using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using System.Data.SqlClient;
using System.Data;
namespace DataAccessLayer
{
    public class DALPersonel
    {
        public static List<EntityPersonel> PersonelListesi()
        {
            List<EntityPersonel> degerler = new List<EntityPersonel>();
            SqlCommand Listele = new SqlCommand("Select * From TBLBILGI", Baglanti.bgl);
            if (Listele.Connection.State != ConnectionState.Open)
            {
                Listele.Connection.Open();
            }
            SqlDataReader dr = Listele.ExecuteReader();
            while (dr.Read())
            {
                EntityPersonel ent = new EntityPersonel();
                ent.Id = int.Parse(dr["ID"].ToString());
                ent.Ad = dr["AD"].ToString();
                ent.Soyad = dr["SOYAD"].ToString();
                ent.Gorev = dr["GOREV"].ToString();
                ent.Sehir = dr["SEHIR"].ToString();
                ent.Maas = short.Parse(dr["MAAS"].ToString());
                degerler.Add(ent);
            }
            dr.Close();
            return degerler;
        }
        public static int PersonleEkle(EntityPersonel p)
        {
            SqlCommand Ekle = new SqlCommand("insert into TBLBILGI (AD,SOYAD,GOREV,SEHIR,MAAS) VALUES (@P1,@P2,@P3,@P4,@P5)", Baglanti.bgl);
            if (Ekle.Connection.State != ConnectionState.Open)
            {
                Ekle.Connection.Open();
            }
            Ekle.Parameters.AddWithValue("@P1", p.Ad);
            Ekle.Parameters.AddWithValue("@P2", p.Soyad);
            Ekle.Parameters.AddWithValue("@P3", p.Gorev);
            Ekle.Parameters.AddWithValue("@P4", p.Sehir);
            Ekle.Parameters.AddWithValue("@P5", p.Maas);
            return Ekle.ExecuteNonQuery();
        }

        public static bool PersonelSıl(int p)
        {
            SqlCommand Sılme = new SqlCommand("Delete From TBLBILGI where ID=@P1", Baglanti.bgl);
            if (Sılme.Connection.State != ConnectionState.Open)
            {
                Sılme.Connection.Open();
            }
            Sılme.Parameters.AddWithValue("@P1", p);
            return Sılme.ExecuteNonQuery() > 0;
        }

        public static bool PersonelGuncelle(EntityPersonel ent)
        {
            SqlCommand Guncelle = new SqlCommand("Update TBLBILGI SET AD=@P1,SOYAD=@P2,MAAS=@P3,SEHIR=@P4,GOREV=@P5 WHERE ID=@P6", Baglanti.bgl);
            {
                if (Guncelle.Connection.State != ConnectionState.Open)
                {
                    Guncelle.Connection.Open();
                }
                Guncelle.Parameters.AddWithValue("@P1", ent.Ad);
                Guncelle.Parameters.AddWithValue("@P2", ent.Soyad);
                Guncelle.Parameters.AddWithValue("@P3", ent.Maas);
                Guncelle.Parameters.AddWithValue("@P4", ent.Sehir);
                Guncelle.Parameters.AddWithValue("@P5", ent.Gorev);
                Guncelle.Parameters.AddWithValue("@P6", ent.Id);
                return Guncelle.ExecuteNonQuery() > 0;
            }
        }

    }
}

