var QuanLyNhaCungCapconfig = {
    pagesize: 10,
    pageindex: 1,
}
var QuanLyNhaCungCapController = {
    init: function () {
        QuanLyNhaCungCapController.loadData();
        QuanLyNhaCungCapController.registerEvent();
    },
    registerEvent: function () {
        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                QuanLyNhaCungCapController.loadData(true);
            }
        });
        $('#btnSearchSS').off('click').on('click', function () {
            QuanLyNhaCungCapController.loadData(true);
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#txtmancc').removeAttr("disabled");
            QuanLyNhaCungCapController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            QuanLyNhaCungCapController.saveData();
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#hidID').val('1');
            $('#txtmancc').attr("disabled", "disabled");
            var MaNCC = $(this).data('id');
            QuanLyNhaCungCapController.getData(MaNCC);
        });
        $('.btn-delete').off('click').on('click', function () {
            var MaNCC = $(this).data('id');
            var cf = confirm('Bạn chắc chắn muốn xóa không?');
            if (cf) {
                QuanLyNhaCungCapController.deleteData(MaNCC);
            }
        })
    },
    loadData: function (changePageSize) {
        var search = $('#txtNameS').val();
        $.ajax({
            url: '/QuanLyNhaCungCap/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                searchName: search,
                page: QuanLyNhaCungCapconfig.pageindex,
                pagesize: QuanLyNhaCungCapconfig.pagesize
            },
            success: function (response) {
                $('#slsp').html(response.sl);
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<tr id="' + item.MaNCC + '">';
                    html += '<td><a href="/Admin/QuanLyNhaCungCap/DsSanPham?ma=' + item.MaNCC + '"> ' + item.MaNCC + '</a></td>';
                    html += '<td><a href="/Admin/QuanLyNhaCungCap/DsSanPham?ma=' + item.MaNCC + '">' + item.TenNCC + '</a></td>';
                    html += '<td>' + item.DiaChi + '</td>';
                    html += '<td>' + item.Email + '</td>';
                    html += '<td>' + item.SoDienThoai + '</td>';
                    html += '<td>\
                                <button class="btn btn-primary btn-edit" data-id="' + item.MaNCC + '"><span class="icon-edit"></span></button>\
                                <button class="btn btn-danger btn-delete" data-id="' + item.MaNCC + '"><span class="icon-remove"></span></button>\
                             </td>';
                    html += '</tr>';
                });
                $('#tblData').html(html);
                QuanLyNhaCungCapController.pagin(response.total, function () {
                    QuanLyNhaCungCapController.loadData();
                }, changePageSize);
                QuanLyNhaCungCapController.registerEvent();
            }
        });
    },
    pagin: function (totalrow, callback, changePageSize) {
        var totalpage = Math.ceil(totalrow / QuanLyNhaCungCapconfig.pagesize);
        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }
        $('#pagination').twbsPagination({
            totalPages: totalpage,
            first: "<<",
            prev: "<",
            next: ">",
            last: ">>",
            visiblePages: 10,
            onPageClick: function (event, page) {
                QuanLyNhaCungCapconfig.pageindex = page;
                setTimeout(callback, 200);
            }
        });
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtmancc').val('');
        $('#txttenncc').val('');
        $('#txtdiachi').val('');
        $('#txtemail').val('');
        $('#txtsdt').val('');
    },
    saveData: function () {
        var id = parseInt($('#hidID').val());
        var MaNCC = $('#txtmancc').val();
        var tenncc = $('#txttenncc').val();
        var diachi = $('#txtdiachi').val();
        var email = $('#txtemail').val();
        var sdt = $('#txtsdt').val();
        var nhacungcap = {
            MaNCC: MaNCC,
            TenNCC: tenncc,
            DiaChi:diachi,
            Email: email,
            SoDienThoai:sdt
        }
        $.ajax({
            url: '/QuanLyNhaCungCap/SaveData',
            type: 'POST',
            dataype: 'json',
            data: {
                strNhaCungCap: JSON.stringify(nhacungcap),
                id: id
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLyNhaCungCapController.loadData(true);
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
    getData: function (MaNCC) {
        $.ajax({
            url: '/QuanLyNhaCungCap/GetData',
            type: 'GET',
            data: {
                MaNCC: MaNCC
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                $('#txtmancc').val(data.MaNCC);
                $('#txttenncc').val(data.TenNCC);
                $('#txtdiachi').val(data.DiaChi);
                $('#txtemail').val(data.Email);
                $('#txtsdt').val(data.SoDienThoai);
            },
            error: function (err) {
                alert('Khong thuc hien duoc');
            }
        });
    },
    deleteData: function (MaNCC) {
        $.ajax({
            url: '/QuanLyNhaCungCap/Delete',
            data: {
                MaNCC: MaNCC
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    QuanLyNhaCungCapController.loadData(true);
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
QuanLyNhaCungCapController.init();