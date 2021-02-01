var QuanLySanPhamconfig ={
    pagesize: 10,
    pageindex: 1
}
var QuanLySanPhamController = {
    init: function () {
        QuanLySanPhamController.loadData();
        QuanLySanPhamController.registerEvent();
    },
    registerEvent: function () {
        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                QuanLySanPhamController.loadData(true);
            }
        });
        $('#btnSearchSS').off('click').on('click', function () {
            QuanLySanPhamController.loadData(true);
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#txtmaloaisp').removeAttr("disabled");
            QuanLySanPhamController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            QuanLySanPhamController.saveData();
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#hidID').val('1');
            $('#txtmaloaisp').attr("disabled", "disabled");
            var maloaisp = $(this).data('id');
            QuanLySanPhamController.getData(maloaisp);
        });
        $('.btn-delete').off('click').on('click', function () {
            var maloaisp = $(this).data('id');
            var cf = confirm('Bạn chắc chắn muốn xóa không?');
            if (cf) {
                QuanLySanPhamController.deleteData(maloaisp);
            }
        })
    },
    loadData: function (changePageSize) {
        var search = $('#txtNameS').val();
        $.ajax({
            url: '/QuanLyLoaiSanPham/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                searchName: search,
                page:QuanLySanPhamconfig.pageindex,
                pagesize:QuanLySanPhamconfig.pagesize
            },
            success: function (response) {
                $('#slsp').html(response.sl);
                var data = response.data;
                var html = '';
                //var template = $('#data-template').html();
                $.each(data, function (i, item) {
                    html += '<tr id="' + item.MaLoaiSP + '">';
                    html += '<td><a href="/Admin/QuanLyLoaiSanPham/DsSanPham?ma=' + item.MaLoaiSP + '"> ' + item.MaLoaiSP + '</a></td>';
                    html += '<td><a href="/Admin/QuanLyLoaiSanPham/DsSanPham?ma=' + item.MaLoaiSP + '">' + item.TenLoai + '</a></td>';
                    html += '<td>' + item.MoTa + '</td>';
                    html += '<td>\
                                <button class="btn btn-primary btn-edit" data-id="'+item.MaLoaiSP+'"><span class="icon-edit"></span></button>\
                                <button class="btn btn-danger btn-delete" data-id="' + item.MaLoaiSP + '"><span class="icon-remove"></span></button>\
                             </td>'; 
                    html += '</tr>';     
                });
               
                $('#tblData').html(html);
                QuanLySanPhamController.pagin(response.total, function () {
                    QuanLySanPhamController.loadData();
                },changePageSize);
                QuanLySanPhamController.registerEvent();
            }
        });
    },
    pagin: function (totalrow, callback,changePageSize) {
        var totalpage = Math.ceil(totalrow / QuanLySanPhamconfig.pagesize);
        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }
        $('#pagination').twbsPagination({
            totalPages: totalpage,
            first: "<<",
            prev:"<",
            next: ">",
            last: ">>",
            visiblePages: 10,
            onPageClick: function (event, page) {
                QuanLySanPhamconfig.pageindex = page;
                setTimeout(callback, 200);
            }
        });
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtmaloaisp').val('');
        $('#txttenloaisp').val('');
        $('#txtmota').val('');
    },
    saveData: function () {
        var id = parseInt($('#hidID').val());
        var maloaisp = $('#txtmaloaisp').val();
        var tenloaisp = $('#txttenloaisp').val();
        var mota = $('#txtmota').val();
        var loaisanpham = {
            MaLoaiSP: maloaisp,
            TenLoai: tenloaisp,
            MoTa:mota
        }
        $.ajax({
            url: '/QuanLyLoaiSanPham/SaveData',
            type: 'POST',
            dataType: 'json',
            data:{
                strLoaiSanPham: JSON.stringify(loaisanpham),
                id: id
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLySanPhamController.loadData(true);
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
    getData: function(maloaisp) {
        $.ajax({
            url: '/QuanLyLoaiSanPham/GetData',
            type: 'GET',
            data: {
                maloaisp: maloaisp
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                $('#txtmaloaisp').val(data.MaLoaiSP);
                $('#txttenloaisp').val(data.TenLoai);
                $('#txtmota').val(data.MoTa);
            },
            error:function(err){
                alert('Khong thuc hien duoc');
            }
        });
    },
    deleteData: function (maloaisp) {
        $.ajax({
            url: '/QuanLyLoaiSanPham/Delete',
            data: {
                maloaisp:maloaisp
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    QuanLySanPhamController.loadData(true);
                }
                else
                {
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