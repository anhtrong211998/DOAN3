var QuanLyNhanVienconfig = {
    pagesize: 10,
    pageindex: 1,
}
var QuanLyNhanVienController = {
    init: function () {
        
        QuanLyNhanVienController.loadData();
        QuanLyNhanVienController.registerEvent();
    },
    registerEvent: function () {
        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                QuanLyNhanVienController.loadData(true);
            }
        });
        $('#btnSearchSS').off('click').on('click', function () {
            QuanLyNhanVienController.loadData(true);
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#txtmanv').removeAttr("disabled");
            QuanLyNhanVienController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            QuanLyNhanVienController.saveData();
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#hidID').val('1');
            $('#txtmanv').attr("disabled", "disabled");
            var mansx = $(this).data('id');
            QuanLyNhanVienController.getData(mansx);
        });
        $('.btn-delete').off('click').on('click', function () {
            var mansx = $(this).data('id');
            var cf = confirm('Bạn chắc chắn muốn xóa không?');
            if (cf) {
                QuanLyNhanVienController.deleteData(mansx);
            }
        })
    },
    loadData: function (changePageSize) {
        var search = $('#txtNameS').val();
        $.ajax({
            url: '/QuanLyNhanVien/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                searchName: search,
                page: QuanLyNhanVienconfig.pageindex,
                pagesize: QuanLyNhanVienconfig.pagesize
            },
            success: function (response) {
                $('#slsp').html(response.sl);
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<tr id="' + item.MaNV + '">';
                    html += '<td>' + item.MaNV + '</td>';
                    html += '<td>' + item.TenNV + '</td>';
                    html += '<td style="text-align:center;"><img class="img-product" src="/Content/products-logo/' + item.HinhAnh + '" style="width:97px;height:60px;border-radius:50%"/></td>';
                    html += '<td>' + item.ChucVu + '</td>';
                    html += '<td style="width:150px;">' + item.DiaChi + '</td>';
                    html += '<td>' + item.Email + '</td>';
                    html += '<td>' + item.SoDienThoai + '</td>';
                    if (item.TaiKhoan == "") {
                        html += '<td>NULL</td>';
                    }
                    else {
                        html += '<td>' + item.TaiKhoan + '</td>';
                    }                 
                    html += '<td>\
                                <button class="btn btn-primary btn-edit" data-id="' + item.MaNV + '"><span class="icon-edit"></span></button>\
                                <button class="btn btn-danger btn-delete" data-id="' + item.MaNV + '"><span class="icon-remove"></span></button>\
                             </td>';
                    html += '</tr>';
                });
                $('#tblData').html(html);
                if (data != null) {
                    QuanLyNhanVienController.pagin(response.total, function () {
                        QuanLyNhanVienController.loadData();
                    }, changePageSize);
                }               
                QuanLyNhanVienController.registerEvent();
            }
        });
    },
    pagin: function (totalrow, callback, changePageSize) {
        
            var totalpage = Math.ceil(totalrow / QuanLyNhanVienconfig.pagesize);
            if ($('#pagination a').length === 0 || changePageSize === true) {
                $('#pagination').empty();
                $('#pagination').removeData("twbs-pagination");
                $('#pagination').unbind("page");
            }
            if (totalpage != null) {
            $('#pagination').twbsPagination({
                totalPages: totalpage,
                first: "<<",
                prev: "<",
                next: ">",
                last: ">>",
                visiblePages: 10,
                onPageClick: function (event, page) {
                    QuanLyNhanVienconfig.pageindex = page;
                    setTimeout(callback, 200);
                }
            });
        }       
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtmanv').val('');
        $('#txttennv').val('');
        $('#HinhAnh1').val('');
        $('#txtchucvu').val('');
        $('#txtquequan').val('');
        $('#txtemail').val('');
        $('#txtsdt').val('');
    },
    saveData: function () {
        var id = parseInt($('#hidID').val());
        var manv = $('#txtmanv').val();
        var tennv = $('#txttennv').val();
        var hinhanh = $('#HinhAnh1').val();
        var chucvu = $('#txtchucvu').val();
        var quequan = $('#txtquequan').val();
        var email = $('#txtemail').val();
        var sdt = $('#txtsdt').val();
        var taikhoan = $('#txttaikhoan').val();
        var nhanvien = {
            MaNV: manv,
            TenNV: tennv,
            HinhAnh: hinhanh,
            ChucVu: chucvu,
            DiaChi: quequan,
            Email: email,
            SoDienThoai: sdt,
            TaiKhoan:taikhoan
        }
        $.ajax({
            url: '/QuanLyNhanVien/SaveData',
            type: 'POST',
            dataype: 'json',
            data: {
                strNhanvien: JSON.stringify(nhanvien),
                id: id
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLyNhanVienController.loadData(true);
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
            url: '/QuanLyNhanVien/GetData',
            type: 'GET',
            data: {
                manv: manv
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                $('#txtmanv').val(data.MaNV);
                $('#txttennv').val(data.TenNV);
                $('#HinhAnh1').val(data.HinhAnh);
                $('#txtchucvu').val(data.ChucVu);
                $('#txtquequan').val(data.DiaChi);
                $('#txtemail').val(data.Email);
                $('#txtsdt').val(data.SoDienThoai);
            },
            error: function (err) {
                alert('Khong thuc hien duoc');
            }
        });
    },
    deleteData: function (manv) {
        $.ajax({
            url: '/QuanLyNhanVien/Delete',
            data: {
                manv: manv
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    QuanLyNhanVienController.loadData(true);
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
QuanLyNhanVienController.init();