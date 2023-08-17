$(document).ready(function () {
    $('#faculty').attr('disabled', true);
    $('#department').attr('disabled', true);
    LoadFaculties();

    $('#faculty').change(function () {
        var facultyId = $(this).val();
        if (facultyId > 0) {
            LoadDepartments(facultyId);
        }
        else {
            alert("Select Faculty");
            $('#department').empty();
            $('#department').attr('disabled', true);
            $('#department').append('<option disabled selected>Select Department</option>');
        }
    })
});


function LoadFaculties() {
    $('#faculty').empty();
    $('#faculty').attr('disabled', false);

    $.ajax({
        url: '/Employer/JobPost/GetFaculties',
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#faculty').attr('disabled', false);
                $('#faculty').append('<option disabled selected>Select Faculty</option>');
                $.each(response, function (i, data) {
                    $('#faculty').append('<option value=' + data.id + '>' + data.name + '</option>');
                });
            }
            else {
                $('#faculty').attr('disabled', true);
                $('#department').attr('disabled', true);
                $('#faculty').append('<option disabled selected>Select Faculty</option>');
                $('#department').append('<option disabled selected>Select Department</option>');
            }
        },
        error: function (error) {
            alert(error);
        }
    });
}


function LoadDepartments(facultyId) {
    $('#department').empty();

    $.ajax({
        url: '/Employer/JobPost/GetDepartments?Id=' + facultyId,
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#department').attr('disabled', false);
                $('#department').append('<option disabled selected>Select Department</option>');
                $.each(response, function (i, data) {
                    $('#department').append('<option value=' + data.id + '>' + data.name + '</option>');
                });
            }
            else {
                $('#department').attr('disabled', true);
                $('#department').append('<option disabled selected>Select Department</option>');
            }
        },
        error: function (error) {
            alert(error);
        }
    });
}