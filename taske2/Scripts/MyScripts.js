function JqueryAjax(form) {
    if ($(form).valid())
    {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                $("#previousEducationContainer").html(response);
            }
        })
    }
    return false;
}