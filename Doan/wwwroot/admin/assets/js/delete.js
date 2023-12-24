$(document).ready(function () {
    let _id; // Thêm biến cục bộ

    $(".btn-delete").click(function () {
        _id = $(this).data("id");

        // Mở modal xác nhận xóa
        $("#deleteConfirmationModal").modal("show");
    });

    // Xác nhận xóa khi nhấn nút Xóa trong modal
    $("#confirmDeleteBtn").click(function () {
        $.ajax({
            url: "/admin/blogs/delete",
            type: "POST",
            data: { id: _id },
            success: function (result) {
                if (result) {
                    $("#tr_" + _id).remove();
                    toastr.success('Xóa thành công');
                } else {
                    toastr.error('Xóa không thành công');
                }
            }

            // Đừng đặt đoạn code đóng modal ở đây
        });
        // Đóng modal sau khi xác nhận xóa
        $("#deleteConfirmationModal").modal("hide");
    });

   
});

