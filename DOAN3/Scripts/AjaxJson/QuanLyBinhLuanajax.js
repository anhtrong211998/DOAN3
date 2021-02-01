var QuanLyBinhLuanconfig = {
    pagesize: 10,
    pageindex: 1,
}
var QuanLyBinhLuanController = {
    init: function () {

        QuanLyBinhLuanController.loadData();
        QuanLyBinhLuanController.registerEvent();
    },
    registerEvent: function () {
        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                QuanLyBinhLuanController.loadData(true);
            }
        });
        $('#btnSearchSS').off('click').on('click', function () {
            QuanLyBinhLuanController.loadData(true);
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#txtmaBL').removeAttr("disabled");
            QuanLyBinhLuanController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            QuanLyBinhLuanController.saveData();
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#hidID').val('1');
            $('#txtmaBL').attr("disabled", "disabled");
            var mansx = $(this).data('id');
            QuanLyBinhLuanController.getData(mansx);
        });
        $('.btn-delete').off('click').on('click', function () {
            var mansx = $(this).data('id');
            var cf = confirm('Bạn chắc chắn muốn xóa không?');
            if (cf) {
                QuanLyBinhLuanController.deleteData(mansx);
            }
        })
    },
    loadData: function (changePageSize) {
        var search = $('#txtNameS').val();
        $.ajax({
            url: '/QuanLyBinhLuan/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                searchName: search,
                page: QuanLyBinhLuanconfig.pageindex,
                pagesize: QuanLyBinhLuanconfig.pagesize
            },
            success: function (response) {
                $('#slsp').html(response.sl);
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<tr id="' + item.MaBL + '">';
                    html += '<td>' + item.MaBL + '</td>';
                    html += '<td style="width:150px;">' + item.NoiDungBL + '</td>';
                    if (item.MaThanhVien == "") {
                        html += '<td>NULL</td>';
                    }
                    else {
                        html += '<td>' + item.MaThanhVien + '</td>';
                    }
                    if (item.MaSP == "") {
                        html += '<td>NULL</td>';
                    }
                    else {
                        html += '<td>' + item.MaSP + '</td>';
                    }                    
                    html += '<td >' + item.DanhGia + '</td>';
                    html += '<td>\
                                <button class="btn btn-primary btn-edit" data-id="' + item.MaBL + '"><span class="icon-edit"></span></button>\
                                <button class="btn btn-danger btn-delete" data-id="' + item.MaBL + '"><span class="icon-remove"></span></button>\
                             </td>';
                    html += '</tr>';
                });
                $('#tblData').html(html);
                if (data != null) {
                    QuanLyBinhLuanController.pagin(response.total, function () {
                        QuanLyBinhLuanController.loadData();
                    }, changePageSize);
                }
                QuanLyBinhLuanController.registerEvent();
            }
        });
    },
    pagin: function (totalrow, callback, changePageSize) {

        var totalpage = Math.ceil(totalrow / QuanLyBinhLuanconfig.pagesize);
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
                    QuanLyBinhLuanconfig.pageindex = page;
                    setTimeout(callback, 200);
                }
            });
        }
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtmaBL').val('');
        $('#txtnoidung').val('');
        $('#txtmaTV').val('');
        $('#txtmaSP').val('');
        $('#txtdanhgia').val('');
    },
    saveData: function () {
        var id = parseInt($('#hidID').val());
        var mabl= $('#txtmaBL').val();
        var noidung = $('#txtnoidung').val();
        var matv = $('#txtmaTV').val();
        var masp = $('#txtmaSP').val();
        var danhgia = $('#txtdanhgia').val();
        var nhanvien = {
            MaBL: mabl,
            NoiDungBL: noidung,
            MaThanhVien: matv,
            MaSP: masp
        }
        $.ajax({
            url: '/QuanLyBinhLuan/SaveData',
            type: 'POST',
            dataype: 'json',
            data: {
                strNhanvien: JSON.stringify(nhanvien),
                danhgia:danhgia,
                id: id
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLyBinhLuanController.loadData(true);
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
            url: '/QuanLyBinhLuan/GetData',
            type: 'GET',
            data: {
                manv: manv
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                $('#txtmaBL').val(data.MaBL);
                $('#txtnoidung').val(data.NoiDungBL);
                $('#txtmaTV').val(data.MaThanhVien);
                $('#txtmaSP').val(data.MaSP);
                $('#txtdanhgia').val(data.DanhGia);
            },
            error: function (err) {
                alert('Khong thuc hien duoc');
            }
        });
    },
    deleteData: function (manv) {
        $.ajax({
            url: '/QuanLyBinhLuan/Delete',
            data: {
                manv: manv
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    QuanLyBinhLuanController.loadData(true);
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
QuanLyBinhLuanController.init();