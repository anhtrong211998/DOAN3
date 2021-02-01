var QuanLyTaiKhoanAdminconfig = {
    pagesize: 10,
    pageindex: 1
}
var QuanLyTaiKhoanAdminController = {
    init: function () {
        QuanLyTaiKhoanAdminController.loadData();
        QuanLyTaiKhoanAdminController.registerEvent();
    },
    registerEvent: function () {
        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                QuanLyTaiKhoanAdminController.loadData(true);
            }
        });
        $('#btnSearchSS').off('click').on('click', function () {
            QuanLyTaiKhoanAdminController.loadData(true);
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#txtusername').removeAttr("disabled");
            QuanLyTaiKhoanAdminController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            QuanLyTaiKhoanAdminController.saveData();
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#hidID').val('1');
            $('#txtusername').attr("disabled", "disabled");
            var username = $(this).data('id');
            QuanLyTaiKhoanAdminController.getData(username);
        });
        $('.btn-delete').off('click').on('click', function () {
            var maloaisp = $(this).data('id');
            var cf = confirm('Bạn chắc chắn muốn xóa không?');
            if (cf) {
                QuanLyTaiKhoanAdminController.deleteData(maloaisp);
            }
        })
    },
    loadData: function (changePageSize) {
        var search = $('#txtNameS').val();
        $.ajax({
            url: '/QuanLyTaiKhoanAdmin/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                searchName: search,
                page: QuanLyTaiKhoanAdminconfig.pageindex,
                pagesize: QuanLyTaiKhoanAdminconfig.pagesize
            },
            success: function (response) {
                $('#slsp').html(response.sl);
                var data = response.data;
                var html = '';
                //var template = $('#data-template').html();
                $.each(data, function (i, item) {
                    html += '<tr id="' + item.UserName + '">';
                    html += '<td>' + item.UserName + '</td>';
                    html += '<td>' + item.MatKhau + '</td>';
                    html += '<td>' + item.Remember + '</td>';
                    html += '<td>\
                                <button class="btn btn-primary btn-edit" data-id="' + item.UserName + '"><span class="icon-edit"></span></button>\
                                <button class="btn btn-danger btn-delete" data-id="' + item.UserName + '"><span class="icon-remove"></span></button>\
                             </td>';
                    html += '</tr>';
                });

                $('#tblData').html(html);
                QuanLyTaiKhoanAdminController.pagin(response.total, function () {
                    QuanLyTaiKhoanAdminController.loadData();
                }, changePageSize);
                QuanLyTaiKhoanAdminController.registerEvent();
            }
        });
    },
    pagin: function (totalrow, callback, changePageSize) {
        var totalpage = Math.ceil(totalrow / QuanLyTaiKhoanAdminconfig.pagesize);
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
                QuanLyTaiKhoanAdminconfig.pageindex = page;
                setTimeout(callback, 200);
            }
        });
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtusername').val('');
        $('#txtpassword').val('');
    },
    saveData: function () {
        var id = parseInt($('#hidID').val());
        var username = $('#txtusername').val();
        var matkhau = $('#txtpassword').val();
        $.ajax({
            url: '/QuanLyTaiKhoanAdmin/SaveData',
            type: 'POST',
            dataType: 'json',
            data: {
                UserName: username,
                MatKhau: matkhau,
                id: id
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLyTaiKhoanAdminController.loadData(true);
                }
                else {
                    alert('Khong thanh cong');
                }
            },
            error: function (err) {
                aler('Khong thuc hien duoc');
            }
        });
    },
    getData: function (username) {
        $.ajax({
            url: '/QuanLyTaiKhoanAdmin/GetData',
            type: 'GET',
            data: {
                username: username
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                $('#txtusername').val(data.UserName);
                $('#txtpassword').val(data.MatKhau);
            },
            error: function (err) {
                alert('Khong thuc hien duoc');
            }
        });
    },
    deleteData: function (username) {
        $.ajax({
            url: '/QuanLyTaiKhoanAdmin/Delete',
            data: {
                username: username
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    QuanLyTaiKhoanAdminController.loadData(true);
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
QuanLyTaiKhoanAdminController.init();