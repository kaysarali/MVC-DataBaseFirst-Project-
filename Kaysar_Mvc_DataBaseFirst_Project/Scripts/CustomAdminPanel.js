$("document").ready(function () {
    $("#PurchaseTableShowlink")[0].click();
    $("#LoadingStatus1").html("Loading Table...");

    $("#AttendanceTableShowlink")[0].click();
    $("#LoadingAttendanceTable").html("Loading Table...");
});

var SearchOnSuccess = function (data) {
    $('#PurchaseCategory').val('');
    $('#txtboxSearch').val('');
    $('#FirstDate').val('');
    $('#SecondDate').val('');
    $('#EmpIdSearch').val('');

};



//======== Image Show =============
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#ImgFile').attr('src', e.target.result)
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function AddNewEmployee(StudentId) {
    $("#form2")[0].reset();
    $('#ImgFile').attr('src', '/images/App_images/noimageavailable.jpg');
    $("#EmployeeId").val(0);
    $("#ModalTitle").html("Add New Student");
    $("#btnEmpSave").val('Insert');
    $("#myModal").modal();
}



function EditEmployee(EmployeeId) {
    var url = "/AdminPanel/GetEmployeeById?EmployeeId=" + EmployeeId;
    $("#ModalTitle").html("Update Employee Record");
    $("#myModal").modal();
    $.ajax({
        type: "Get",
        url: url,
        success: function (data) {

            var obj = JSON.parse(data);
            var todayDate = new Date(obj.DateOfBirth).toISOString().slice(0, 10);
            $("#EmployeeId").val(obj.EmployeeId);
            $("#EmpName").val(obj.EmpName);
            $("#EmpUserName").val(obj.EmpUserName);
            $("#EmpAddress").val(obj.EmpAddress);

            $("#DateOfBirth").val(todayDate);

            $("#EmpEmail").val(obj.EmpEmail);
            $("#EmpPassword").val(obj.EmpPassword);

            $("#LoginTime").val(obj.LoginTime);
            $("#LogoutTime").val(obj.LogoutTime);

            $('#ImgFile').attr('src', obj.ImgPath);

 
            $("#DropDwn option:selected").val(obj.RoletbId);
            $("#btnEmpSave").val('Update');
        }

    })
}

function taskDate(dateMilli) {
    var d = (new Date(dateMilli) + '').split(' ');
    d[2] = d[2] + ',';

    return [d[0], d[1], d[2], d[3]].join(' ');
}


function dateFormat(d) {
    return ((d.getMonth() + 1) + "").padStart(2, "0")
        + "/" + (d.getDate() + "").padStart(2, "0")
        + "/" + d.getFullYear();
}

//======== Insert Product Successfully Massage =============
var getresult = function (data) {
    $("#PurchaseTableShowlink")[0].click();
    $("#EmployeeId").val(0);

    $("#EmpName").val("");
    $("#EmpUserName").val("");
    $("#EmpAddress").val("");
    $("#DateOfBirth").val("");
    $("#EmpEmail").val("");
    $("#EmpPassword").val("");

    $("#LoginTime").val("");
    $("#LogoutTime").val("");

    $("#DropDwn option:selected").val("");


    $('#upimage').val('');
    $('#ImgFile').attr('src', '/images/App_images/noimageavailable.jpg');
    
    $("#btnEmpSave").val('Insert');
    $("#myModal").modal("hide");

};



var DeleteEmployee = function (EmployeeId) {
    $("#DelEmployeeId").val(EmployeeId);
    $("#DeleteConfirmation").modal();
}



var ConfirmDelete = function () {
    var DelEmployeeId = $("#DelEmployeeId").val();
    $.ajax({
        type: "POST",
        url: "/AdminPanel/DeleteEmployee?EmployeeId=" + DelEmployeeId,
        success: function (result) {
            $("#PurchaseTableShowlink")[0].click();
            $("#DeleteConfirmation").modal("hide");
        }
    })
}
