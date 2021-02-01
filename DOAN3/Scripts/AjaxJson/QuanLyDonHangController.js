var QuanLyDonHangconfig = {
    pagesize: 10,
    pageindex: 1,
}
var QuanLyDonHangController = {
    init: function () {

        QuanLyDonHangController.loadData();
        QuanLyDonHangController.registerEvent();
    },
    registerEvent: function () {
        $('#txtNameS').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                QuanLyDonHangController.loadData(true);
            }
        });
        $('#btnSearchSS').off('click').on('click', function () {
            QuanLyDonHangController.loadData(true);
        });
        $('#btnAddNew').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#btnUpdate').hide();
            $('#btnSave').show();
            QuanLyDonHangController.resetForm();
        });
        $('#btnSave').off('click').on('click', function () {
            QuanLyDonHangController.NhapHang();
        });
        $('.btn-edit').off('click').on('click', function () {
            $('#modalADDUPDATE').modal('show');
            $('#btnUpdate').show();
            $('#btnSave').hide();
            $('#hidID').val('1');
            QuanLyDonHangController.updateform();
            var mansx = $(this).data('id');
            QuanLyDonHangController.getData(mansx);
        });
        $('#btnUpdate').off('click').on('click', function () {
            QuanLyDonHangController.saveData();
        });
        $('.btn-delete').off('click').on('click', function () {
            var mansx = $(this).data('id');
            var cf = confirm('Bạn chắc chắn muốn xóa không?');
            if (cf) {
                QuanLyDonHangController.deleteData(mansx);
            }
        })
    },
    loadData: function (changePageSize) {
        var search = $('#txtNameS').val();
        $.ajax({
            url: '/Admin/QuanLyDonHang/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                searchName: search,
                page: QuanLyDonHangconfig.pageindex,
                pagesize: QuanLyDonHangconfig.pagesize
            },
            success: function (response) {
                $('#slsp').html(response.sl);
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    var date = new Date(parseInt(item.NgayDat.substr(6)));
                    var month = date.getMonth() + 1;
                    var day = date.getDate();
                    var year = date.getFullYear();
                    item.NgayDat = day + "/" + month + "/" + year;
                    html += '<tr id="' + item.MaDonDatHang + '">';
                    html += '<td><a href="/Admin/QuanLyCTPhieuNhap/DanhSachCTPN?ma=' + item.MaDonDatHang + '"> ' + item.MaDonDatHang + '</a></td>';
                    html += '<td>' + item.NgayDat + '</td>';
                    html += '<td>' + item.TinhTrangGiaoHang + '</td>';
                    html += '<td>' + item.NgayGiao + '</td>';
                    html += '<td>' + item.DaThanhToan + '</td>';
                    html += '<td>' + item.MaKH + '</td>';
                    html += '<td>' + item.MaNV + '</td>';
                    html += '<td>' + item.UuDai + '</td>';
                    html += '<td>' + item.ThanhTien + '</td>';
                    html += '<td>\
                                <button class="btn btn-primary btn-edit" data-id="' + item.MaDonDatHang + '"><span class="icon-edit"></span></button>\
                                <button class="btn btn-danger btn-delete" data-id="' + item.MaDonDatHang + '"><span class="icon-remove"></span></button>\
                             </td>';
                    html += '</tr>';
                });
                $('#tblData').html(html);
                if (data != null) {
                    QuanLyDonHangController.pagin(response.total, function () {
                        QuanLyDonHangController.loadData();
                    }, changePageSize);
                }
                QuanLyDonHangController.registerEvent();
            }
        });
    },
    pagin: function (totalrow, callback, changePageSize) {

        var totalpage = Math.ceil(totalrow / QuanLyDonHangconfig.pagesize);
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
                    QuanLyDonHangconfig.pageindex = page;
                    setTimeout(callback, 200);
                }
            });
        }
    },
    resetForm: function () {
        $('#hidID').val('0');
        $('#txtmanpn').attr("disabled", "disabled");
        $('#txtngaydat').attr("disabled", "disabled");
        $('#txttinhtrang').attr("disabled", "disabled");
        $('#txtngaygiao').attr("disabled", "disabled");
        $('#txtthanhtoan').attr("disabled", "disabled");
        $('#txtmakhachhang').val('');
        $('#txtmanv').attr("disabled", "disabled");
        $('#txtuudai').val('');
        $('#txtmasp').val('');
        $('#txtdg').val('');
        $('#txtsl').val('');
    },
    updateform: function () {
        $('#txtmanpn').removeAttr("disabled", "disabled");
        $('#txtngaydat').removeAttr("disabled", "disabled");
        $('#txttinhtrang').removeAttr("disabled", "disabled");
        $('#txtngaygiao').removeAttr("disabled", "disabled");
        $('#txtthanhtoan').removeAttr("disabled", "disabled");
        $('#txtmanv').removeAttr("disabled", "disabled");
        $('#txtmanpn').attr("disabled", "disabled");
        $('#txtmasp').attr("disabled", "disabled");
        $('#txtdg').attr("disabled", "disabled");
        $('#txtsl').attr("disabled", "disabled");
    },
    NhapHang: function () {
        var id = parseInt($('#hidID').val());
        var mankh = $('#txtmakhachhang').val();
        var uudai = $('#txtuudai').val();
        var masp = $('#txtmasp').val();
        var soluong = $('#txtsl').val();
        var dongia = $('#txtdg').val();
        $.ajax({
            url: '/Admin/QuanLyDonHang/NhapHang',
            type: 'POST',
            dataype: 'json',
            data: {
                mankh: mankh,
                uudai: uudai,
                masp: masp,
                soluong: soluong,
                dongia: dongia
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLyDonHangController.loadData(true);
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
        var ngaydat = $('#txtngaydat').val();
        var tinhtrang = $('#txttinhtrang').prop('checked');
        var ngaygiao = $('#txtngaygiao').val();
        var thanhtoan = $('#txtthanhtoan').prop('checked');
        var makh = $('#txtmakhachhang').val();
        var manv = $('#txtmanv').val();
        var uudai = $('#txtuudai').val();
        var nhanvien = {
            NgayDat: ngaydat,
            TinhTrangGiaoHang: tinhtrang,
            ngaygiao: ngaygiao,
            DaThanhToan: thanhtoan,
            MaKH: makh,
            MaNV: manv,
        }
        $.ajax({
            url: '/Admin/QuanLyDonHang/SaveData',
            type: 'POST',
            dataype: 'json',
            data: {
                strPN: JSON.stringify(nhanvien),
                id: id
            },
            success: function (response) {
                if (response.success) {
                    $('#modalADDUPDATE').modal('hide');
                    QuanLyDonHangController.loadData(true);
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
            url: '/Admin/QuanLyDonHang/GetData',
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
            url: '/Admin/QuanLyDonHang/Delete',
            data: {
                maloaisp: manv
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    QuanLyDonHangController.loadData(true);
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
QuanLyDonHangController.init();