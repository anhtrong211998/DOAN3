var QuanLyThanhVienconfig = {
    pagesize: 10,
    pageindex: 1,
}
var QuanLyThanhVienController = {
    init: function () {

        QuanLyThanhVienController.loadData();
        QuanLyThanhVienController.registerEvent();
    },
    registerEvent: function () {
        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                QuanLyThanhVienController.loadData(true);
            }
        });
        $('#btnSearchSS').off('click').on('click', function () {
            QuanLyThanhVienController.loadData(true);
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#txtemail').removeAttr("disabled");
            QuanLyThanhVienController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            QuanLyThanhVienController.saveData();
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#hidID').val('1');
            $('#txtemail').attr("disabled", "disabled");
            var mansx = $(this).data('id');
            QuanLyThanhVienController.getData(mansx);
        });
        $('.btn-delete').off('click').on('click', function () {
            var mansx = $(this).data('id');
            var cf = confirm('Bạn chắc chắn muốn xóa không?');
            if (cf) {
                QuanLyThanhVienController.deleteData(mansx);
            }
        })
    },
    loadData: function (changePageSize) {
        var search = $('#txtNameS').val();
        $.ajax({
            url: '/QuanLyThanhVien/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                searchName: search,
                page: QuanLyThanhVienconfig.pageindex,
                pagesize: QuanLyThanhVienconfig.pagesize
            },
            success: function (response) {
                $('#slsp').html(response.sl);
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<tr id="' + item.Email + '">';
                    html += '<td>' + item.Email + '</td>';
                    html += '<td>' + item.MatKhau + '</td>';
                    html += '<td>' + item.HoTen + '</td>';
                    html += '<td style="width:150px;">' + item.DiaChi + '</td>';
                    html += '<td>' + item.SoDienThoai + '</td>';
                    
                    html += '<td>\
                                <button class="btn btn-primary btn-edit" data-id="' + item.Email + '"><span class="icon-edit"></span></button>\
                                <button class="btn btn-danger btn-delete" data-id="' + item.Email + '"><span class="icon-remove"></span></button>\
                             </td>';
                    html += '</tr>';
                });
                $('#tblData').html(html);
                if (data != null) {
                    QuanLyThanhVienController.pagin(response.total, function () {
                        QuanLyThanhVienController.loadData();
                    }, changePageSize);
                }
                QuanLyThanhVienController.registerEvent();
            }
        });
    },
    pagin: function (totalrow, callback, changePageSize) {

        var totalpage = Math.ceil(totalrow / QuanLyThanhVienconfig.pagesize);
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
                    QuanLyThanhVienconfig.pageindex = page;
                    setTimeout(callback, 200);
                }
            });
        }
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtemail').val('');
        $('#txtmatkhau').val('');
        $('#txttenkh').val('');
        $('#txtsdt').val('');
        $('#txtdiachi').val('');
    },
    saveData: function () {
        var id = parseInt($('#hidID').val());
        var makh = $('#txtemail').val();
        var tenkh = $('#txttenkh').val();
        var matkhau = $('#txtmatkhau').val();
        var sdt = $('#txtsdt').val();
        var diachi = $('#txtdiachi').val();
        var nhanvien = {
            Email: makh,
            HoTen: tenkh,
            SoDienThoai: sdt,
            DiaChi: diachi,
        }
        $.ajax({
            url: '/QuanLyThanhVien/SaveData',
            type: 'POST',
            dataype: 'json',
            data: {
                strKhachHang: JSON.stringify(nhanvien),
                matkhau: matkhau,
                id: id
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLyThanhVienController.loadData(true);
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
            url: '/QuanLyThanhVien/GetData',
            type: 'GET',
            data: {
                MaKH: manv
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                $('#txtemail').val(data.Email);
                $('#txtmatkhau').val(data.MatKhau)
                $('#txttenkh').val(data.TenKH);
                $('#txtsdt').val(data.SoDienThoai);
                $('#txtdiachi').val(data.DiaChi);
            },
            error: function (err) {
                alert('Khong thuc hien duoc');
            }
        });
    },
    deleteData: function (manv) {
        $.ajax({
            url: '/QuanLyThanhVien/Delete',
            data: {
                MaKH: manv
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    QuanLyThanhVienController.loadData(true);
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
QuanLyThanhVienController.init();