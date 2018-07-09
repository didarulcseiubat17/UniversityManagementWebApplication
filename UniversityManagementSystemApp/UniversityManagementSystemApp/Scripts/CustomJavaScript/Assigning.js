$(document).ready(function() {
    var teacherId = $('#teacherId');
    teacherId.append('<option value=0>Select Teacher</option>');
    $('#courseTextBox').append("<option>Select Course</option>");
    teacherId.prop('disabled', true);
    $('#courseTextBox').prop('disabled', true);

    $('#departmentId').change(function() {
        teacherId.empty();
        $("#courseTextBox").empty();
        $("#DeptErrorLabel").html("");
        var jsonData = $(this).val();
        $.ajax({
            url: "@Url.Action("GetTeacherByDepartmentId", "CourseAssignTo")",
            method: "POST",
            data: { departmentId: jsonData },
            dataType: "json",
            success: function(data) {
                teacherId.append('<option value=0>Select Teacher</option>');
                $.each(data, function(index, value) {
                    teacherId.prop('disabled', false);
                    //teacherId.append($("</option>", { value: val.Id, Text: val.TeacherName }));
                    $("#teacherId").append('<option value=' + value.Id + '>' + value.TeacherName + '</option>');
                });
            },
            error: function() {
                alert("There Is an Error");
            }
        });
        //for comboBox
        $.ajax({
            url: "@Url.Action("GetCourseCodeByDepartmentId", "CourseAssignTo")",
            method: "POST",
            data: { departmentId: jsonData },
            dataType: "json",
            success: function(data) {
                $('#courseTextBox').append("<option>Select Course</option>");
                $('#courseTextBox').prop('disabled', false);
                $.each(data, function(index, value) {
                    $("#courseTextBox").append('<option value=' + value.Id + '>' + value.CourseCode + '</option>');
                });
            },
            error: function() {
                alert("There Is an Error");
            }
        });
//for Checkbox...


        $.ajax({
            url: "@Url.Action("GetAllCourseCode", "CourseAssignTo")",
            type: "GET",
            data: { departmentId: jsonData },
            dataType: "json",
            success: function(data) {
                var result = "";
                $.each(data, function(index, value) {
                    var checkBoxes = "<input type='checkbox' name='courses' value='" + value.CourseCredit + "'>" + value.CourseCode + "<br>";
                    result += checkBoxes;
                });
                $('#updateCourseCheckBoxes').html(result);
                ////The Programming Just Begain...

                var addCheckedBoxesValues = function(value) {
                    var checkedBox = $('input[name="' + value + '"]:checked');
                    var temp = 0;
                    checkedBox.each(function() {
                        var checkedBoxValue = $(this).val();
                        var parsedInteger = parseInt(checkedBoxValue);
                        temp += parsedInteger;
                        //$('#xyz').html(temp);
                    });
                    return temp;
                }
                var checkBox = $('input[name="courses"]');
                checkBox.click(function() {
                    var x = addCheckedBoxesValues($(this).attr('name'));
                    $('#xyz').html(x);
                });
            },
            error: function() {
                alert("Oops! There Is an Error.");
            }
        });
    });

    $('#teacherId').change(function() {
        if ($(this).val() == 0) {
            $('#teacherCustomError').html("Please Select A Teacher Name.");
            return false;
        } else {
            $.ajax({
                url: "@Url.Action("GetCreditInformationByTeacherId", "CourseAssignTo")",
                method: "POST",
                data: { teacherId: $(this).val() },
                dataType: "json",
                success: function(data) {
                    $('#creditTaken').val(data.CreditTaken);
                    $('#remainingCredit').val(data.CreditLeft);
                },
                error: function() {
                    alert("There Is An Error.");
                }
            });
        }
        $('#teacherCustomError').html("");
    });

    $('#courseTextBox').change(function() {
        var courseId = $(this).val();
        $('#customCourseIdError').html("");
        if ($('#courseTextBox option:selected').val() === "Select Course") {
            $("#nameId").val("");
            $("#creditId").val("");
            $('#customCourseIdError').html("Please Select A Course Name");
            return false;
        } else {
            $.ajax({
                url: "@Url.Action("GetCourseInformationById", "CourseAssignTo")",
                method: "POST",
                data: { courseId: courseId },
                dataType: "json",
                success: function(data) {
                    $('#nameId').val(data.CourseName);
                    $('#creditId').val(data.CourseCredit);
                },
                error: function() {
                    alert("There Is An Error.");
                }
            });
        }
    });
});