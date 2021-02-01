
var QuanLySanPhamController = {
    init: function () {
        
        QuanLySanPhamController.loadData(null,null,null);
        QuanLySanPhamController.registerEvent();
    },
    registerEvent: function () {
        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                var searchname=$(this).val();
                QuanLySanPhamController.loadData(null, null, searchname);
            }
        });
        $('#btnSearchSS').off('click').on('click', function () {
            var searchname = $('#txtNameS').val();
            QuanLySanPhamController.loadData(null, null, searchname);
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#txtmasp').removeAttr("disabled");
            QuanLySanPhamController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            QuanLySanPhamController.saveData();
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#hidID').val('1');
            $('#txtmasp').attr("disabled", "disabled");
            var mansx = $(this).data('id');
            QuanLySanPhamController.getData(mansx);
        });
        $('.btn-delete').off('click').on('click', function () {
            var mansx = $(this).data('id');
            var cf = confirm('Bạn chắc chắn muốn xóa không?');
            if (cf) {
                QuanLySanPhamController.deleteData(mansx);
            }
        })
    },
    loadData: function (mal, chisotrang,search) {
        var search = $('#txtNameS').val();
        $.ajax({
            url: '/Admin/QuanLySanPham/SanPhamPartial',
            type: 'GET',
            dataType: 'html',
            data: {
                
                maloai: mal,
                page:chisotrang,
                search: search,
            },
            success: function (data) {
                $('#renderhere').html(data);
                QuanLySanPhamController.pagin();
                QuanLySanPhamController.registerEvent();
            }
        });
    },
    pagin: function () {
        $('.pageIndex').click(function () {
            event.preventDefault();
            //var makh = $('#selectedid').val();
            var chisotrang = $(this).data('id');
            alert(chisotrang);
            //lay tu ma loai hien tai

            QuanLySanPhamController.loadData(null, chisotrang,null);
        });
        $(".prev").click(function () {
            var page = $(".pageIndex.active").data('id');
            alert(page);
            var chisotrang = page - 1;
            if (chisotrang < 1) {
                chisotrang = 1;
            }
            //var makh = $('#selectedid').find(':selected').val();
            //alert(makh);
            QuanLySanPhamController.loadData(null, chisotrang,null);
        });
        $(".next").click(function () {
            var page = $(".pageIndex.active").data('id');
            alert(page);
            var chisotrangmax = $(".pageIndex").length;

            var chisotrang = page + 1;
            if (chisotrang > chisotrangmax) {
                chisotrang = chisotrangmax;
            }
            //var makh = $('#selectedid').find(':selected').data('id');
            //alert(makh);
            QuanLySanPhamController.loadData(null, chisotrang,null);
        });
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtmasp').val('');
        $('#txttensp').val('');
        $('#txtmota').val('');
        $('#txtdongia').val('');
        $('#txtgiamgia').val('');
        $('#HinhAnh1').val('');
        $('#txtsoluong').val('');
        $('#txtngaycapnhat').val('');
    },
    saveData: function () {
        var id = parseInt($('#hidID').val());
        var masp = $('#txtmasp').val();
        var tensp = $('#txttensp').val();
        var mota = CKEDITOR.instances.txtmota.getData();
        mota = $.trim(mota);
        mota = mota.replace('&#39', '&#27');
        console.log(mota);
        var dongia = $('#txtdongia').val();
        var giamgia = $('#txtgiamgia').val();
        var hinhanh = $('#HinhAnh1').val();
        var soluong = $('#txtsoluong').val();
        var ngaycapnhat = $('#txtngaycapnhat').val();
        var mancc = $('#txtMaNCC').val();
        var mansx = $('#txtMaNSX').val();
        var maloaisp = $('#txtMaLoaiSP').val();
        var sanpham = {
            MaSP: masp,
            TenSP: tensp,
            MoTa: mota,
            DonGia: dongia,
            GiamGia: giamgia,
            HinhAnh: hinhanh,
            SoLuongTon: soluong,
            NgayCapNhat: ngaycapnhat,
            MaNCC: mancc,
            MaNSX: mansx,
            MaLoaiSP:maloaisp
        }
        $.ajax({
            url: '/Admin/QuanLySanPham/SaveData',
            type: 'POST',
            dataype: 'json',
            data: {
                strSanPham: JSON.stringify(sanpham),
                id: id
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLySanPhamController.loadData(null,null,null);
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
    getData: function (manv) {
        $.ajax({
            url: '/Admin/QuanLySanPham/GetData',
            type: 'GET',
            data: {
                manv: manv
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                var date = new Date(parseInt(data.NgayCapNhat.substr(6)));
                var month = date.getMonth() + 1;
                var day = date.getDate();
                var year = date.getFullYear();
                data.NgayCapNhat = day + "/" + month + "/" + year;
                $('#txtmasp').val(data.MaSP);
                $('#txttensp').val(data.TenSP);
                $('#txtmota').val(CKEDITOR.instances.txtmota.setData(data.MoTa));
                $('#txtdongia').val(data.DonGia);
                $('#txtgiamgia').val(data.GiamGia);
                $('#HinhAnh1').val(data.HinhAnh);
                $('#txtsoluong').val(data.SoLuongTon);
                $('#txtngaycapnhat').val(data.NgayCapNhat);
                $('#txtMaNCC').val(data.MaNCC);
                $('#txtMaNSX').val(data.MaNSX);
                $('#txtMaLoaiSP').val(data.MaLoaiSP);
            },
            error: function (err) {
                alert('Khong thuc hien duoc');
            }
        });
    },
    deleteData: function (manv) {
        $.ajax({
            url: '/Admin/QuanLySanPham/Delete',
            data: {
                manv: manv
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    QuanLySanPhamController.loadData(null,null,null);
                }
                else {
                    alert(response.ms);
                }
            },
            error: function (err) {
                alert('Khong thuc hien duoc');
            }
        });
    }
}
QuanLySanPhamController.init();