// Thêm sự kiện khi tài liệu đã được tải hoàn toàn
document.addEventListener("DOMContentLoaded", function () {
    // Lấy danh sách các menu cha
    var parentMenus = document.querySelectorAll('.menu-links .nav > li');

    // Lặp qua danh sách menu cha
    parentMenus.forEach(function (parentMenu) {
        // Thêm sự kiện cho sự kiện "mouseenter" và "mouseleave"
        parentMenu.addEventListener("mouseenter", function () {
            // Hiển thị menu con khi rê chuột vào menu cha
            var subMenu = parentMenu.querySelector('.sub-menu');
            if (subMenu) {
                subMenu.style.display = 'block';
            }
        });

        parentMenu.addEventListener("mouseleave", function () {
            // Ẩn menu con khi rê chuột ra khỏi menu cha
            var subMenu = parentMenu.querySelector('.sub-menu');
            if (subMenu) {
                subMenu.style.display = 'none';
            }
        });
    });
});
