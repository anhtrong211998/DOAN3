//var QuanLyChiTietPhieuNhapconfig = {
//    pagesize: 10,
//    pageindex: 1,
//}
var QuanLyChiTietPhieuNhapController = {
    init: function () {
        
        //QuanLyChiTietPhieuNhapController.loadData();
        QuanLyChiTietPhieuNhapController.registerEvent();
    },
    registerEvent: function () {
        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                QuanLyChiTietPhieuNhapController.loadData(true);
            }
        });
        $('#btnSearchSS').off('click').on('click', function () {
            QuanLyChiTietPhieuNhapController.loadData(true);
        });
        //$('#btnAddNew').off('click').on('click', function () {
        //    $('#modalADDUPDATE').modal('show');
        //    $('#btnUpdate').hide();
        //    $('#btnSave').show();
        //    $('#txtmanpn').attr("disabled", "disabled");
        //    $('#txttt').attr("disabled", "disabled");
        //    QuanLyChiTietPhieuNhapController.resetForm();
        //});
        //$('#btnSave').off('click').on('click', function () {
        //    QuanLyChiTietPhieuNhapController.NhapHang();
        //});
        $('.btn-edit').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#btnUpdate').show();
            $('#btnSave').hide();
            $('#hidID').val('1');
            $('#txtmanpn').attr("disabled", "disabled");
            $('#txtMaNCC').attr("disabled", "disabled");
            $('#txtmasp').attr("disabled", "disabled");
            $('#txtngaycapnhat').attr("disabled", "disabled");
            var mansx = $(this).data('id');
            QuanLyChiTietPhieuNhapController.getData(mansx);
        });
        $('#btnUpdate').off('click').on('click', function () {
            var masp = $('#txtmasp').val();
            var mapn = $('#txtmanpn').val();
            QuanLyChiTietPhieuNhapController.saveData();
        });
        //$('.btn-delete').off('click').on('click', function () {
        //    var mansx = $(this).data('id');
        //    var cf = confirm('Bạn chắc chắn muốn xóa không?');
        //    if (cf) {
        //        QuanLyChiTietPhieuNhapController.deleteData(mansx);
        //    }
        //})
    },
    //loadData: function (chisotrang, search) {
    //    var search = $('#txtNameS').val();
    //    $.ajax({
    //        url: '/Admin/QuanLyCTPhieuNhap/DanhSachChiTietPN',
    //        type: 'GET',
    //        dataType: 'html',
    //        data: {

    //            //ma: mal,
    //            page: chisotrang,
    //            search: search,
    //        },
    //        success: function (data) {
    //            $('#renderhere').html(data);
    //            QuanLyChiTietPhieuNhapController.pagin();
    //            QuanLyChiTietPhieuNhapController.registerEvent();
    //        }
    //    });
    //},
    //pagin: function () {
    //    $('.pageIndex').click(function () {
    //        event.preventDefault();
    //        //var makh = $('#selectedid').val();
    //        var chisotrang = $(this).data('id');
    //        alert(chisotrang);
    //        //lay tu ma loai hien tai

    //        QuanLyChiTietPhieuNhapController.loadData( chisotrang, null);
    //    });
    //    $(".prev").click(function () {
    //        var page = $(".pageIndex.active").data('id');
    //        alert(page);
    //        var chisotrang = page - 1;
    //        if (chisotrang < 1) {
    //            chisotrang = 1;
    //        }
    //        //var makh = $('#selectedid').find(':selected').val();
    //        //alert(makh);
    //        QuanLyChiTietPhieuNhapController.loadData(chisotrang, null);
    //    });
    //    $(".next").click(function () {
    //        var page = $(".pageIndex.active").data('id');
    //        alert(page);
    //        var chisotrangmax = $(".pageIndex").length;

    //        var chisotrang = page + 1;
    //        if (chisotrang > chisotrangmax) {
    //            chisotrang = chisotrangmax;
    //        }
    //        //var makh = $('#selectedid').find(':selected').data('id');
    //        //alert(makh);
    //        QuanLyChiTietPhieuNhapController.loadData(chisotrang, null);
    //    });
    //},
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtmanpn').val('');
        $('#txtMaNCC').val('');
        $('#txtmasp').val('');
        $('#txtngaycapnhat').val('');      
        $('#txtsl').val('');
        $('#txtdg').val('');
        $('#txttt').val('');
       
    },
    saveData: function () {
        var id = parseInt($('#hidID').val());
        var mapn = $('#txtmanpn').val();
        var mancc = $('#txtMaNCC').val();
        var masp = $('#txtmasp').val();
        var ngaynhap = $('#txtngaycapnhat').val();
        var thanhtien = $('#txttt').val();
        var soluong = $('#txtsl').val();
        var dongia = $('#txtdg').val();
        //var thanhtien = $('#txttt').val();
        //var nhanvien = {
        //    MaPhieuNhap: mapn,
        //    MaNCC: mancc,
        //    NgayNhap: ngaynhap,
        //    //ThanhTien: thanhtien,
        //    masp: masp,
        //    soluongnhap: soluong,
        //    dongia:dongia
        //}
        $.ajax({
            url: '/Admin/QuanLyCTPhieuNhap/SaveData',
            type: 'POST',
            dataype: 'json',
            data: {
                maphieunhap: mapn,
                mancc: mancc,
                ngaynhap: ngaynhap,
                //thanhtien: thanhtien,
                masp: masp,
                soluongnhap: soluong,
                dongia: dongia
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    window.location = '/Admin/QuanLyCTPhieuNhap/DanhSachCTPN?ma=' + mapn + '';
                }
                else {
                    alert(response.ms);
                }
            },
            error: function (err) {
                alert('Khong thuc hien duoc');
            }
        });
    },
    getData: function (mapn) {
        $.ajax({
            url: '/Admin/QuanLyCTPhieuNhap/GetData',
            type: 'GET',
            data: {
                ma: mapn
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                $('#txtmanpn').val(data.MaPhieuNhap)
                $('#txtMaNCC').val(data.PhieuNhap.MaNCC);
                $('#txtmasp').val(data.MaSP);
                var date = new Date(parseInt(data.PhieuNhap.NgayNhap.substr(6)));
                var month = date.getMonth() + 1;
                var day = date.getDate();
                var year = date.getFullYear();
                data.PhieuNhap.NgayNhap = day + "/" + month + "/" + year;
                $('#txtngaycapnhat').val(data.PhieuNhap.NgayNhap);
                $('#txtsl').val(data.SoLuongNhap);
                $('#txtdg').val(data.DonGia);
                $('#txttt').val(data.PhieuNhap.ThanhTien);

            },
            error: function (err) {
                alert('Khong thuc hien duoc');
            }
        });
    },
    //deleteData: function (manv,masp) {
    //    $.ajax({
    //        url: '/Admin/QuanLyCTPhieuNhap/Delete',
    //        data: {
    //            manv: manv,
    //            masp:masp
    //        },
    //        type: 'POST',
    //        dataType: 'json',
    //        success: function (response) {
    //            if (response.success) {
    //                //QuanLyChiTietPhieuNhapController.loadData(true);
    //            }
    //            else {
    //                alert(response.ms);
    //            }
    //        },
    //        error: function (err) {
    //            alert('Khong thuc hien duoc');
    //        }
    //    });
    //}
}
QuanLyChiTietPhieuNhapController.init();