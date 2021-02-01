var QuanLyNhaSanXuatconfig = {
    pagesize: 10,
    pageindex:1,
}
var QuanLyNhaSanXuatController = {
    init: function () {
        QuanLyNhaSanXuatController.loadData();
        QuanLyNhaSanXuatController.registerEvent();
    },
    registerEvent: function () {
        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                QuanLyNhaSanXuatController.loadData(true);
            }
        });
        $('#btnSearchSS').off('click').on('click', function () {
            QuanLyNhaSanXuatController.loadData(true);
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#txtmansx').removeAttr("disabled");
            QuanLyNhaSanXuatController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            QuanLyNhaSanXuatController.saveData();
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#hidID').val('1');
            $('#txtmansx').attr("disabled", "disabled");
            var mansx = $(this).data('id');
            QuanLyNhaSanXuatController.getData(mansx);
        });
        $('.btn-delete').off('click').on('click', function () {
            var mansx = $(this).data('id');
            var cf = confirm('Bạn chắc chắn muốn xóa không?');
            if (cf) {
                QuanLyNhaSanXuatController.deleteData(mansx);
            }
        })
    },
    loadData: function (changePageSize) {
        var search = $('#txtNameS').val();
        $.ajax({
            url: '/QuanLyNhaSanXuat/LoadData',
            type: 'GET',
            dataType: 'json',
            data:{
                searchName: search,
                page: QuanLyNhaSanXuatconfig.pageindex,
                pagesize: QuanLyNhaSanXuatconfig.pagesize
            },
            success: function (response) {
                $('#slsp').html(response.sl);
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<tr id="' + item.MaNSX + '">';
                    html += '<td><a href="/Admin/QuanLyNhaSanXuat/DanhSachSanPham?ma=' + item.MaNSX + '"> ' + item.MaNSX + '</a></td>';
                    html += '<td><a href="/Admin/QuanLyNhaSanXuat/DanhSachSanPham?ma=' + item.MaNSX + '">' + item.TenNSX + '</a></td>';
                    html += '<td style="text-align:center;"><img class="img-product" src="/Content/products-logo/' + item.Logo + '" style="width:150px;height:60px;"/></td>';
                    html += '<td>\
                                <button class="btn btn-primary btn-edit" data-id="' + item.MaNSX + '"><span class="icon-edit"></span></button>\
                                <button class="btn btn-danger btn-delete" data-id="' + item.MaNSX + '"><span class="icon-remove"></span></button>\
                             </td>';
                    html += '</tr>';
                });
                $('#tblData').html(html);
                QuanLyNhaSanXuatController.pagin(response.total, function () {
                    QuanLyNhaSanXuatController.loadData();
                }, changePageSize);
                QuanLyNhaSanXuatController.registerEvent();
            }
        });
    },
    pagin: function (totalrow, callback, changePageSize) {
        var totalpage = Math.ceil(totalrow / QuanLyNhaSanXuatconfig.pagesize);
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
                QuanLyNhaSanXuatconfig.pageindex = page;
                setTimeout(callback, 200);
            }
        });
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtmansx').val('');
        $('#txttennsx').val('');
        $('#HinhAnh1').val('');
    },
    saveData: function () {
        var id = parseInt($('#hidID').val());
        var mansx = $('#txtmansx').val();
        var tennsx = $('#txttennsx').val();
        var logo = $('#HinhAnh1').val();
        var nhasanxuat = {
            MaNSX: mansx,
            TenNSX: tennsx,
            Logo: logo
        }
        $.ajax({
            url: '/QuanLyNhaSanXuat/SaveData',
            type: 'POST',
            dataype: 'json',
            data: {
                strNhaSanXuat: JSON.stringify(nhasanxuat),
                id:id
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLyNhaSanXuatController.loadData(true);
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
    getData: function (mansx) {
        $.ajax({
            url: '/QuanLyNhaSanXuat/GetData',
            type: 'GET',
            data: {
                mansx:mansx
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                $('#txtmansx').val(data.MaNSX);
                $('#txttennsx').val(data.TenNSX);
                $('#HinhAnh1').val(data.Logo);
            },
            error: function (err) {
                alert('Khong thuc hien duoc');
            }
        });
    },
    deleteData: function (mansx) {
        $.ajax({
            url: '/QuanLyNhaSanXuat/Delete',
            data: {
                mansx: mansx
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    QuanLyNhaSanXuatController.loadData(true);
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
QuanLyNhaSanXuatController.init();