using System;
using System.Collections.Generic;
using System.Text;

// Lớp cơ sở cho tất cả nhân viên
public abstract class NhanVien
{
    public string HoTen { get; set; }
    public int NamSinh { get; set; }
    public string BangCap { get; set; }

    public NhanVien(string hoTen, int namSinh, string bangCap)
    {
        HoTen = hoTen;
        NamSinh = namSinh;
        BangCap = bangCap;
    }

    public abstract double TinhLuong();

    public virtual void NhapThongTin()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.Write("Nhập họ tên: ");
        HoTen = Console.ReadLine();
        Console.Write("Nhập năm sinh: ");
        NamSinh = int.Parse(Console.ReadLine());
        Console.Write("Nhập bằng cấp: ");
        BangCap = Console.ReadLine();
    }

    public virtual void HienThi()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine($"Họ tên: {HoTen}");
        Console.WriteLine($"Năm sinh: {NamSinh}");
        Console.WriteLine($"Bằng cấp: {BangCap}");
    }
}

// Lớp cho nhân viên quản lý
public class NhaQuanLy : NhanVien
{
    public string ChucVu { get; set; }
    public int SoNgayCong { get; set; }
    public double BacLuong { get; set; }

    public NhaQuanLy(string hoTen, int namSinh, string bangCap)
        : base(hoTen, namSinh, bangCap) { }

    public override double TinhLuong()
    {
        return SoNgayCong * BacLuong;
    }

    public override void NhapThongTin()
    {
        Console.OutputEncoding = Encoding.UTF8;
        base.NhapThongTin();
        Console.Write("Nhập chức vụ: ");
        ChucVu = Console.ReadLine();
        Console.Write("Nhập số ngày công: ");
        SoNgayCong = int.Parse(Console.ReadLine());
        Console.Write("Nhập bậc lương: ");
        BacLuong = double.Parse(Console.ReadLine());
    }

    public override void HienThi()
    {
        Console.OutputEncoding = Encoding.UTF8;
        base.HienThi();
        Console.WriteLine($"Chức vụ: {ChucVu}");
        Console.WriteLine($"Số ngày công: {SoNgayCong}");
        Console.WriteLine($"Bậc lương: {BacLuong}");
        Console.WriteLine($"Lương: {TinhLuong()}");
    }
}

// Lớp cho nhà khoa học
public class NhaKhoaHoc : NhaQuanLy
{
    public int SoBaiBao { get; set; }

    public NhaKhoaHoc(string hoTen, int namSinh, string bangCap)
        : base(hoTen, namSinh, bangCap) { }

    public override void NhapThongTin()
    {
        Console.OutputEncoding = Encoding.UTF8;
        base.NhapThongTin();
        Console.Write("Nhập số bài báo đã công bố: ");
        SoBaiBao = int.Parse(Console.ReadLine());
    }

    public override void HienThi()
    {
        Console.OutputEncoding = Encoding.UTF8;
        base.HienThi();
        Console.WriteLine($"Số bài báo: {SoBaiBao}");
    }
}

// Lớp cho nhân viên phòng thí nghiệm
public class NhanVienPhongThiNghiem : NhanVien
{
    public double LuongKhoan { get; set; }

    public NhanVienPhongThiNghiem(string hoTen, int namSinh, string bangCap)
        : base(hoTen, namSinh, bangCap) { }

    public override double TinhLuong()
    {
        return LuongKhoan;
    }

    public override void NhapThongTin()
    {
        Console.OutputEncoding = Encoding.UTF8;
        base.NhapThongTin();
        Console.Write("Nhập lương khoán: ");
        LuongKhoan = double.Parse(Console.ReadLine());
    }

    public override void HienThi()
    {
        Console.OutputEncoding = Encoding.UTF8;
        base.HienThi();
        Console.WriteLine($"Lương khoán: {LuongKhoan}");
    }
}

// Lớp quản lý danh sách nhân viên
public class QuanLyNhanVien
{
    private List<NhanVien> danhSachNhanVien;

    public QuanLyNhanVien()
    {
        danhSachNhanVien = new List<NhanVien>();
    }

    public void NhapDanhSach()
    {
        while (true)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("\n1. Nhập nhà khoa học");
            Console.WriteLine("2. Nhập nhà quản lý");
            Console.WriteLine("3. Nhập nhân viên phòng thí nghiệm");
            Console.WriteLine("4. Kết thúc nhập");
            Console.Write("Chọn loại nhân viên: ");

            int luaChon = int.Parse(Console.ReadLine());
            if (luaChon == 4) break;

            NhanVien nv = null;
            switch (luaChon)
            {
                case 1:
                    nv = new NhaKhoaHoc("", 0, "");
                    break;
                case 2:
                    nv = new NhaQuanLy("", 0, "");
                    break;
                case 3:
                    nv = new NhanVienPhongThiNghiem("", 0, "");
                    break;
            }

            if (nv != null)
            {
                nv.NhapThongTin();
                danhSachNhanVien.Add(nv);
            }
        }
    }

    public void HienThiDanhSach()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("\nDanh sách nhân viên:");
        foreach (var nv in danhSachNhanVien)
        {
            Console.WriteLine("------------------------");
            nv.HienThi();
        }
    }

    public void TinhTongLuongTheoLoai()
    {
        double tongLuongNhaKhoaHoc = 0;
        double tongLuongNhaQuanLy = 0;
        double tongLuongNhanVienPTN = 0;

        foreach (var nv in danhSachNhanVien)
        {
            if (nv is NhaKhoaHoc)
                tongLuongNhaKhoaHoc += nv.TinhLuong();
            else if (nv is NhaQuanLy && !(nv is NhaKhoaHoc))
                tongLuongNhaQuanLy += nv.TinhLuong();
            else if (nv is NhanVienPhongThiNghiem)
                tongLuongNhanVienPTN += nv.TinhLuong();
        }
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("\nTổng lương theo loại:");
        Console.WriteLine($"Tổng lương nhà khoa học: {tongLuongNhaKhoaHoc}");
        Console.WriteLine($"Tổng lương nhà quản lý: {tongLuongNhaQuanLy}");
        Console.WriteLine($"Tổng lương nhân viên phòng thí nghiệm: {tongLuongNhanVienPTN}");
    }
}

// Lớp chương trình chính
public class Program
{
    public static void Main(string[] args)
    {
        QuanLyNhanVien qlnv = new QuanLyNhanVien();
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== CHƯƠNG TRÌNH QUẢN LÝ NHÂN SỰ VIỆN KHOA HỌC ===");
        qlnv.NhapDanhSach();
        qlnv.HienThiDanhSach();
        qlnv.TinhTongLuongTheoLoai();
    }
}