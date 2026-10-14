using System;
using System.Collections.Generic;

namespace SistemKaryawan
{
    
    public class Karyawan
    {
        public string Nama { get; set; }
        public double Gaji { get; set; }

        public Karyawan(string nama, double gaji)
        {
            Nama = nama;
            Gaji = gaji;
        }

        public virtual void Kerja()
        {
            Console.WriteLine($"{Nama} sedang bekerja.");
        }

        public virtual void InfoKaryawan()
        {
            Console.WriteLine($"Nama: {Nama}, Gaji: {Gaji:N0}");
        }
    }

    
    public class Tetap : Karyawan
    {
        public double Tunjangan { get; set; }

        public Tetap(string nama, double gaji, double tunjangan) : base(nama, gaji)
        {
            Tunjangan = tunjangan;
        }

        public double HitungGajiTotal() => Gaji + Tunjangan;

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} (Karyawan Tetap) bekerja sesuai jam kantor.");
        }

        public override void InfoKaryawan()
        {
            base.InfoKaryawan();
            Console.WriteLine($"Tunjangan: {Tunjangan:N0}, Gaji Total: {HitungGajiTotal():N0}");
        }
    }

    public class Manager : Tetap
    {
        public Manager(string nama, double gaji, double tunjangan) : base(nama, gaji, tunjangan) { }

        public void Memimpin()
        {
            Console.WriteLine($"{Nama} sedang memimpin rapat tim.");
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} (Manager) merencanakan strategi dan mengawasi tim.");
        }
    }

    public class Staff : Tetap
    {
        public Staff(string nama, double gaji, double tunjangan) : base(nama, gaji, tunjangan) { }

        public void KerjakanTugas()
        {
            Console.WriteLine($"{Nama} sedang mengerjakan tugas harian.");
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} (Staff) mengerjakan tugas operasional.");
        }
    }

    
    public class Kontrak : Karyawan
    {
        public int Durasi { get; set; } // dalam bulan

        public Kontrak(string nama, double gaji, int durasi) : base(nama, gaji)
        {
            Durasi = durasi;
        }

        public void CekKontrak()
        {
            Console.WriteLine($"Kontrak {Nama} berlaku selama {Durasi} bulan.");
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} (Kontrak) bekerja selama masa kontrak.");
        }

        public override void InfoKaryawan()
        {
            base.InfoKaryawan();
            Console.WriteLine($"Durasi Kontrak: {Durasi} bulan");
        }
    }

    public class Magang : Kontrak
    {
        public Magang(string nama, double gaji, int durasi) : base(nama, gaji, durasi) { }

        public void Belajar()
        {
            Console.WriteLine($"{Nama} sedang belajar dari mentor.");
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} (Magang) membantu pekerjaan sambil belajar.");
        }
    }

    public class Freelancer : Kontrak
    {
        public Freelancer(string nama, double gaji, int durasi) : base(nama, gaji, durasi) { }

        public void AmbilProyek()
        {
            Console.WriteLine($"{Nama} mengambil proyek baru.");
        }

        public override void Kerja()
        {
            Console.WriteLine($"{Nama} (Freelancer) mengerjakan proyek secara mandiri.");
        }
    }

    
    public class Perusahaan
    {
        private List<Karyawan> daftarKaryawan = new List<Karyawan>();

        public void TambahKaryawan(Karyawan karyawan)
        {
            daftarKaryawan.Add(karyawan);
        }

        public void DaftarKaryawan()
        {
            Console.WriteLine("=== DAFTAR KARYAWAN ===");
            foreach (var k in daftarKaryawan)
            {
                k.InfoKaryawan();
                Console.WriteLine();
            }
        }

        public List<Karyawan> GetKaryawan() => daftarKaryawan;
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            
            Perusahaan perusahaan = new Perusahaan();

          
            Manager manager = new Manager("Budi", 15000000, 5000000);
            Staff staff = new Staff("Siti", 8000000, 2000000);
            Magang magang = new Magang("Andi", 2000000, 6);
            Freelancer freelancer = new Freelancer("Dewi", 6000000, 3);

            
            perusahaan.TambahKaryawan(manager);
            perusahaan.TambahKaryawan(staff);
            perusahaan.TambahKaryawan(magang);
            perusahaan.TambahKaryawan(freelancer);

          
            perusahaan.DaftarKaryawan();

           
            Console.WriteLine("POLYMORPHISM");
            foreach (Karyawan k in perusahaan.GetKaryawan())
            {
                k.Kerja();
            }

           
            Console.WriteLine("\nMETHOD KHUSUS");
            manager.Memimpin();
            staff.KerjakanTugas();
            magang.Belajar();
            freelancer.AmbilProyek();
            magang.CekKontrak();

           
            Console.WriteLine("\nSoal 1");
            manager.Kerja();
            freelancer.Kerja();

            Console.WriteLine("\nSoal 2");
            manager.Memimpin();

            Console.WriteLine("\nSoal 3");
            manager.InfoKaryawan();

            Console.WriteLine("\nSoal 4");
            magang.Belajar();

            Console.WriteLine("\nSoal 5");
            Karyawan k5 = new Staff("Rina", 7500000, 1500000);
            k5.Kerja();
            // k5.KerjakanTugas();            // ERROR compile: Karyawan tidak punya KerjakanTugas()
            ((Staff)k5).KerjakanTugas();       // OK dengan casting

            Console.ReadLine();
        }
    }
}
