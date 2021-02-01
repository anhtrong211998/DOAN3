var GioHangController = {
    init: function () {
       
        GioHangController.registerEvent();
        //$('.xoagiohang').off('click').on('click', function () {
        //    var masp = $(this).data('id');
        //    alert(masp);
        //    GioHangController.XoaGioHang(masp);
        //});
    },
    registerEvent: function () {
        $('.btn-cart').off('click').on('click', function () {
            var masp = $(this).data('id');
            alert(masp);
            GioHangController.ThemGioHang(masp);
        });
        
    },
    ThemGioHang: function (masp) {
        $.ajax({
            url: '/Giohang/ThemGioHang',
            type:'POST',
            datatype:'json',
            data: {
                masp:masp
            },
            success: function (response) {
                $('#cart-total').html(response.Soluong);
                $('#totalprice').html(response.tongtien);
                var data=response.data;
                var html = '';
                $.each(data, function (i, item) {
                    item.TenSP = item.TenSP.substring(0, 20);
                    html += '<li class="item even" id="xoa_' + item.MaSP + '">';
                    html += '<a class="product-image" href="/XemSanpham/XemSanphamChitiet?ma=' + item.MaSP + '" title="Downloadable Product "><img alt="Sample Product" src="/Content/products-images/'+item.HinhAnh+'" width="50" style="height: 50px;margin-top: -13px;"></a>';
                    html += '<div class="detail-item">';
                    html += '<div class="product-details">';
                    html += '<span title="Remove This Item" data-id="' + item.MaSP + '" onclick="XoaGioHang(\'' + item.MaSP + '\')" class="icon-remove xoagiohang" >&nbsp;</span>';
                    html += '<p class="product-name"><a href="/XemSanpham/XemSanphamChitiet?ma=' + item.MaSP + '" title="Downloadable Product">' + item.TenSP + '</a></p>';
                    html += '</div>';
                    html += '<div class="product-details-bottom"><span class="price">' + item.DonGia + '</span></div>';
                    html += '</div>';
                    html += '</li>';
                });
                $('#cart-sidebar').html(html);
            },
            Error: function (err) {
                alert("Không thực hiện được");
            }
        });
    },
    //XoaGioHang: function (masp) {
    //    $.ajax({
    //        url: '/Giohang/XoaGioHang',
    //        type:'POST',
    //        datatype:'json',
    //        data: {
    //            masp: masp
    //        },
    //        success: function (response) {
    //            $('#xoa_' + masp).remove();
    //            $('#cart-total').html(response.soluong);
    //            $('.top-subtotal').html(response.tongtien);
    //        }
    //    });
    //}
}
GioHangController.init();