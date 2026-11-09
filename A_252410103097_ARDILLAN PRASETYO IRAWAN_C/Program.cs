using System;
using System.Collections.Generic;

namespace UTS_PBO_TipeA
{
    class KaryawanBebek
    {
        public string IdBebek { get; set; }
        public string NamaBebek { get; set; }
        public int JumlahTugas { get; private set; }

        public KaryawanBebek(string id, string nama, int jumlahTugas)
        {
            IdBebek = id;
            NamaBebek = nama;
            JumlahTugas = jumlahTugas;
        }

        public void TerimaTugas (int jumlah)
        {
            JumlahTugas += jumlah;
        }

        public void TampilInfo()
        {
            Console.WriteLine($"ID Bebek           : {IdBebek}");
            Console.WriteLine($"Nama Bebek         : {NamaBebek}");
            Console.WriteLine($"Jumlah Tugas         : {JumlahTugas}");
        }
    }
    abstract class Pekerjaan
    {
        private string idPekerjaan;
        private string namaPekerjaan;
        private int stokTugas;

        public Pekerjaan(string id, string nama, int stok)
        {
            idPekerjaan = id;
            namaPekerjaan = nama;
            stokTugas = stok;
        }

        public string IdPekerjaan
        {
            get { return idPekerjaan; }
            set { idPekerjaan = value; }
        }

        public string NamaPekerjaan
        {
            get { return namaPekerjaan; }
            set { namaPekerjaan = value; }
        }

        public int StokTugas
        {
            get { return stokTugas; }
            private set
            {
                if (value < 0)
                {
                    Console.WriteLine("Stok tidak boleh negatif! Nilai diabaikan.");
                    return;
                }
                stokTugas = value;
            }
        }
        public bool KurangiStok(int jumlah)
        {
            if (jumlah <= 0)
            {
                Console.WriteLine("Jumlah pengurangan harus lebih dari 0.");
                return false;
            }
            if (jumlah > stokTugas)
            {
                Console.WriteLine("Jumlah pengurangan melebihi stok yang tersedia.");
                return false;
            }
            StokTugas = stokTugas - jumlah;
            return true;
        }
        public abstract int HitungBiaya(int jumlah);

        public virtual void TampilInfo()
        {
            Console.WriteLine($"ID Pekerjaan  : {IdPekerjaan}");
            Console.WriteLine($"Nama Pekerjaan: {NamaPekerjaan}");
            Console.WriteLine($"Stok Tugas: {StokTugas}");
        }
    }
    class PekerjaanMagang : Pekerjaan
    {
        public PekerjaanMagang(string id, string nama, int stok) : base(id, nama, stok) { }

        public override int HitungBiaya(int jumlah)
        {
            return jumlah * 10000;
        }

        public override void TampilInfo()
        {
            base.TampilInfo();
            Console.WriteLine("Tugas       : fotokopi dokumen, membuat kopi untuk atasan, \r\ndan menyusun laporan yang tidak pernah dibaca");
            Console.WriteLine("Biaya     : Rp10.000 / tugas");
        }
    }

    class PekerjaanSenior : Pekerjaan
    {
        public string JenisKeahlian { get; set; }

        public PekerjaanSenior(string id, string nama, int stok, string jenisKeahlian) : base(id, nama, stok)
        {
            JenisKeahlian = jenisKeahlian;
        }

        public override int HitungBiaya(int jumlah)
        {
            return jumlah * 50000;
        }

        public override void TampilInfo()
        {
            base.TampilInfo();
            Console.WriteLine($"Jenis Keahlian: {JenisKeahlian}");
            Console.WriteLine("Biaya     : Rp50.000 / tugas");
        }
    }
    interface DapatDitugaskan
    {
        bool BagikanTugas();
    }

    class DistribusiTugas : DapatDitugaskan
    {
        public KaryawanBebek karyawanpenerimatugas { get; set; }
        public Pekerjaan pekerjaanyangdibagikan { get; set; }
        public int Jumlah { get; set; }

        public DistribusiTugas(KaryawanBebek karyawanbebek, Pekerjaan pekerjaan, int jumlah)
        {
            karyawanpenerimatugas = karyawanbebek;
            pekerjaanyangdibagikan = pekerjaan;
            Jumlah = jumlah;
        }

        public bool BagikanTugas()
        {
            if (Jumlah <= 0)
            {
                Console.WriteLine("Distribusi GAGAL: jumlah pekerjaan harus lebih dari 0.");
                return false;
            }
            if (Jumlah > Pekerjaan.stokTugas)
            {
                Console.WriteLine($"Distribusi GAGAL: jumlah ({Jumlah}) melebihi stok tugas ({Pekerjaan.stokTugas}).");
                return false;
            }
            Pekerjaan.KurangiStok(Jumlah);
            KaryawanBebek.JumlahTugasDibagikan += Jumlah;
            Console.WriteLine($"Distribusi Tugas berhasil! Sebanyak {Jumlah} tugas yang telah diselesaikan {KaryawanBebek.NamaBebek}");
            return true;
        }
    }