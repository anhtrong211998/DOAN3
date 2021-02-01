var QuanLyKhachHangconfig = {
    pagesize: 10,
    pageindex: 1,
}
var QuanLyKhachHangController = {
    init: function () {

        QuanLyKhachHangController.loadData();
        QuanLyKhachHangController.registerEvent();
    },
    registerEvent: function () {
        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                QuanLyKhachHangController.loadData(true);
            }
        });
        $('#btnSearchSS').off('click').on('click', function () {
            QuanLyKhachHangController.loadData(true);
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#txtemail').removeAttr("disabled");
            QuanLyKhachHangController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            QuanLyKhachHangController.saveData();
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#hidID').val('1');
            $('#txtemail').attr("disabled", "disabled");
            var mansx = $(this).data('id');
            QuanLyKhachHangController.getData(mansx);
        });
        $('.btn-delete').off('click').on('click', function () {
            var mansx = $(this).data('id');
            var cf = confirm('Bạn chắc chắn muốn xóa không?');
            if (cf) {
                QuanLyKhachHangController.deleteData(mansx);
            }
        })
    },
    loadData: function (changePageSize) {
        var search = $('#txtNameS').val();
        $.ajax({
            url: '/QuanLyKhacHang/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                searchName: search,
                page: QuanLyKhachHangconfig.pageindex,
                pagesize: QuanLyKhachHangconfig.pagesize
            },
            success: function (response) {
                $('#slsp').html(response.sl);
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<tr id="' + item.Email + '">';
                    html += '<td>' + item.Email + '</td>';
                    html += '<td>' + item.TenKH + '</td>';
                    html += '<td>' + item.SoDienThoai + '</td>';
                    html += '<td style="width:150px;">' + item.DiaChiGiaoHang + '</td>';
                    html += '<td>\
                                <button class="btn btn-primary btn-edit" data-id="' + item.Email + '"><span class="icon-edit"></span></button>\
                                <button class="btn btn-danger btn-delete" data-id="' + item.Email + '"><span class="icon-remove"></span></button>\
                             </td>';
                    html += '</tr>';
                });
                $('#tblData').html(html);
                if (data != null) {
                    QuanLyKhachHangController.pagin(response.total, function () {
                        QuanLyKhachHangController.loadData();
                    }, changePageSize);
                }
                QuanLyKhachHangController.registerEvent();
            }
        });
    },
    pagin: function (totalrow, callback, changePageSize) {

        var totalpage = Math.ceil(totalrow / QuanLyKhachHangconfig.pagesize);
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
                    QuanLyKhachHangconfig.pageindex = page;
                    setTimeout(callback, 200);
                }
            });
        }
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtemail').val('');
        $('#txttenkh').val('');
        $('#txtsdt').val('');
        $('#txtdiachi').val('');
    },
    saveData: function () {
        var id = parseInt($('#hidID').val());
        var makh = $('#txtemail').val();
        var tenkh = $('#txttenkh').val();
        var sdt = $('#txtsdt').val();
        var diachi = $('#txtdiachi').val();
        var nhanvien = {
            Email: makh,
            TenKH: tenkh,
            SoDienThoai: sdt,
            DiaChiGiaoHang: diachi,
        }
        $.ajax({
            url: '/QuanLyKhacHang/SaveData',
            type: 'POST',
            dataype: 'json',
            data: {
                strKhachHang: JSON.stringify(nhanvien),
                id: id
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLyKhachHangController.loadData(true);
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
            url: '/QuanLyKhacHang/GetData',
            type: 'GET',
            data: {
                MaKH: manv
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                $('#txtemail').val(data.Email);
                $('#txttenkh').val(data.TenKH);
                $('#txtsdt').val(data.SoDienThoai);
                $('#txtdiachi').val(data.DiaChiGiaoHang);
            },
            error: function (err) {
                alert('Khong thuc hien duoc');
            }
        });
    },
    deleteData: function (manv) {
        $.ajax({
            url: '/QuanLyKhacHang/Delete',
            data: {
                MaKH: manv
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    QuanLyKhachHangController.loadData(true);
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
QuanLyKhachHangController.init();