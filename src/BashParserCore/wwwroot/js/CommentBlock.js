
$(document).ready(function () {
    $('.toAnswer-click').click(function () {
        var id = $(this).attr('id');
        $("#comment_" + id).toggle();
    });
});

