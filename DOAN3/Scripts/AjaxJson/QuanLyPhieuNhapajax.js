var QuanLyPhieuNhapconfig = {
    pagesize: 10,
    pageindex: 1,
}
var QuanLyPhieuNhapController = {
    init: function () {
        
        QuanLyPhieuNhapController.loadData();
        QuanLyPhieuNhapController.registerEvent();
    },
    registerEvent: function () {
        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                QuanLyPhieuNhapController.loadData(true);
            }
        });
        $('#btnSearchSS').off('click').on('click', function () {
            QuanLyPhieuNhapController.loadData(true);
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#btnUpdate').hide();
            $('#btnSave').show();
            $('#txtmanpn').attr("disabled", "disabled");
            $('#txttt').attr("disabled", "disabled");
            QuanLyPhieuNhapController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            QuanLyPhieuNhapController.NhapHang();
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#btnUpdate').show();
            $('#btnSave').hide();
            $('#hidID').val('1');
            $('#txtmansx').attr("disabled", "disabled");
            $('#txtdg').attr("disabled", "disabled");
            $('#txtsl').attr("disabled", "disabled");
            var mansx = $(this).data('id');
            QuanLyPhieuNhapController.getData(mansx);
        });
        $('#btnUpdate').off('click').on('click', function () {
            QuanLyPhieuNhapController.saveData();
        });
        $('.btn-delete').off('click').on('click', function () {
            var mansx = $(this).data('id');
            var cf = confirm('Bạn chắc chắn muốn xóa không?');
            if (cf) {
                QuanLyPhieuNhapController.deleteData(mansx);
            }
        })
    },
    loadData: function (changePageSize) {
        var search = $('#txtNameS').val();
        $.ajax({
            url: '/Admin/QuanLyPhieuNhap/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                searchName: search,
                page: QuanLyPhieuNhapconfig.pageindex,
                pagesize: QuanLyPhieuNhapconfig.pagesize
            },
            success: function (response) {
                $('#slsp').html(response.sl);
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    var date = new Date(parseInt(item.NgayNhap.substr(6)));
                    var month = date.getMonth() + 1;
                    var day = date.getDate();
                    var year = date.getFullYear();
                    item.NgayNhap = day + "/" + month + "/" + year;
                    html += '<tr id="' + item.MaPhieuNhap + '">';
                    html += '<td><a href="/Admin/QuanLyCTPhieuNhap/DanhSachCTPN?ma=' + item.MaPhieuNhap + '"> ' + item.MaPhieuNhap + '</a></td>';
                    html += '<td>' + item.MaNCC + '</td>';
                    html += '<td>' + item.NgayNhap + '</td>';
                    html += '<td>' + item.ThanhTien + '</td>';
                    html += '<td>\
                                <button class="btn btn-primary btn-edit" data-id="' + item.MaPhieuNhap + '"><span class="icon-edit"></span></button>\
                                <button class="btn btn-danger btn-delete" data-id="' + item.MaPhieuNhap + '"><span class="icon-remove"></span></button>\
                             </td>';
                    html += '</tr>';
                });
                $('#tblData').html(html);
                if (data != null) {
                    QuanLyPhieuNhapController.pagin(response.total, function () {
                        QuanLyPhieuNhapController.loadData();
                    }, changePageSize);
                }               
                QuanLyPhieuNhapController.registerEvent();
            }
        });
    },
    pagin: function (totalrow, callback, changePageSize) {
        
            var totalpage = Math.ceil(totalrow / QuanLyPhieuNhapconfig.pagesize);
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
                    QuanLyPhieuNhapconfig.pageindex = page;
                    setTimeout(callback, 200);
                }
            });
        }       
    },
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
    NhapHang: function(){
        var id = parseInt($('#hidID').val());
        var mancc = $('#txtMaNCC').val();
        var masp = $('#txtmasp').val();
        var ngaynhap = $('#txtngaycapnhat').val();
        var soluong = $('#txtsl').val();
        var dongia = $('#txtdg').val();
        $.ajax({
            url: '/Admin/QuanLyPhieuNhap/NhapHang',
            type: 'POST',
            dataype: 'json',
            data: {
                mancc: mancc,
                masp: masp,
                ngaynhap:ngaynhap,
                soluongnhap: soluong,
                dongia: dongia
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLyPhieuNhapController.loadData(true);
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
    saveData: function () {
        var id = parseInt($('#hidID').val());
        var mapn = $('#txtmanpn').val();
        var mancc = $('#txtMaNCC').val();
        var masp = $('#txtmasp').val();
        var ngaynhap = $('#txtngaycapnhat').val();
        var thanhtien = $('#txttt').val();
        var soluong = $('#txtsl').val();
        var dongia = $('#txtdg').val();
        var nhanvien = {
            MaPhieuNhap: mapn,
            MaNCC: mancc,
            NgayNhap: ngaynhap,
            ThanhTien: thanhtien,
        }
        $.ajax({
            url: '/Admin/QuanLyPhieuNhap/SaveData',
            type: 'POST',
            dataype: 'json',
            data: {
                strPN: JSON.stringify(nhanvien),
                id: id
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLyPhieuNhapController.loadData(true);
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
            url: '/Admin/QuanLyPhieuNhap/GetData',
            type: 'GET',
            data: {
                maloaisp: manv
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                $('#txtmanpn').val(data.MaPhieuNhap)
                $('#txtMaNCC').val(data.MaNCC);
                var date = new Date(parseInt(data.NgayNhap.substr(6)));
                var month = date.getMonth() + 1;
                var day = date.getDate();
                var year = date.getFullYear();
                data.NgayNhap = day + "/" + month + "/" + year;
                $('#txtngaycapnhat').val(data.NgayNhap);
                $('#txttt').val(data.ThanhTien);;
            },
            error: function (err) {
                alert('Khong thuc hien duoc');
            }
        });
    },
    deleteData: function (manv) {
        $.ajax({
            url: '/Admin/QuanLyPhieuNhap/Delete',
            data: {
                maloaisp: manv
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    QuanLyPhieuNhapController.loadData(true);
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
QuanLyPhieuNhapController.init();