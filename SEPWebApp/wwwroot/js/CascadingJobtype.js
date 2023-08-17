$(document).ready(function () {

    $('#jobtype').attr('disabled', true);
    $('#weekhour').attr('disabled', true);
/*    LoadJobType();*/

    $('#jobtype').change(function () {
        var obid = this.value;
        $('#weekhour').empty();
        $.ajax({
            type: 'Get',
            url: '@Url.Action("getweekhour","JobPost")',
            datatype: 'json',
            data: { id: obid },
            success: function (week) {

                if (week != 6) {
                    $('#weekhour').attr('disabled', false);
                    $.each(week, function (index, item) {
                        $('#weekhour').append('<option value="' + item.id + '">' + item.name + '</option')
                    });
                }
                else {
                    $('#weekhour').attr('disabled', true);
                }
            },
            error: function (error) {
                alert(error);
            }
        });
    });
})