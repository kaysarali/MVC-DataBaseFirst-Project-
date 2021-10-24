$("document").ready(function () {
    $("#PurchaseTableShowlink")[0].click();
    $("#LoadingStatus1").html("Loading Table...");
});


//PurchaseTableDataMethod();
function PurchaseTableDataMethod() {
    $("#LoadingStatus").html("Loading...");
    $.get("/Purchase/GetPurchaseTableData", "", DataBind);
}

function DataBind(studenList) {
    $('#setStudentList').empty();
    $.each(studenList, function (key, value) {
        $('#setStudentList').append(
            "<tr class=''>" +

            "<td>" + value.PurchaseId + "</td>" +
            "<td>" + value.VoucherNo + "</td>" +
            "<td>" + dateFormat(new Date(parseInt((value.PurchaseDate).match(/\d+/)[0]))) + "</td>" +
            "<td>" + value.ProductName + " " + value.ModelName + "</td>" +
            "<td>" + value.SupplierName + "</td>" +
            "<td>" + value.Quantity + "</td>" +
            "<td>" + value.TotalPrice / value.Quantity + "</td>" +
            "<td>" + value.TotalPrice + "</td>" +
            "<td>" + value.EmpName + "</td>" +
            "<td>" + "<img class='productimage' src= '" + value.ImgPath + "' ID='ImageID' height='30' width='30'>" + "</td>" +

            "<td>" +
            "<a class='btn btn-primary' onclick = 'EditPurchase(" + value.PurchaseId + ")' > Edit </a> &nbsp;" +
            "<a class='btn btn-success' onclick='DetailsPurchase(" + value.PurchaseId + ")'> <i class='glyphicon glyphicon-eye-open'></i> </a> &nbsp;" +
            "<a class='btn btn-danger' onclick='DeletePurchase(" + value.PurchaseId + ")'> <i class='glyphicon glyphicon-trash'></i> </a></td> </tr>" +

        $("#LoadingStatus").html("")
        );
    });

    function dateFormat(d) {
        return ((d.getMonth() + 1) + "").padStart(2, "0")
            + "/" + (d.getDate() + "").padStart(2, "0")
            + "/" + d.getFullYear();
    }
   
}


//EditMethod()

//function editmethod() {
//    $.ajax({
//        type: "post",
//        url: "/purchase/getproductmodelmethod",
//        datatype: "json",
//        data: {},
//        success: function (data) {
//            $.each(data, function (key, value) {
//                $('#setstudentlist').append(
//                    "<td>" + value.purchaseid + "</td>" +
//                    "<td>" + value.purchasedate + "</td>"
//                );
//            });
//        }
//    });
//}


//======== Load Product Model dropdown list Open =============
$(function () {
    $('#ddlProductName').change(function () {
        $('#ddlProductModel').empty();
        $.ajax({
            type: "POST",
            url: "/Purchase/GetProductModelMethod",
            datatype: "Json",
            data: { ProductId: $('#ddlProductName').val() },
            success: function (data) {
                $.each(data, function (key, value) {
                    $('#ddlProductModel').append('<option value="' + value.ProductModelId + '">' + value.ModelName + '</option>');
                });
            }
        });
    });
});

//======== Load Product dropdown list Open =============
ProductNameMethod();
function ProductNameMethod() {
    $('#ddlProductName').empty();
    $.ajax({
        type: "POST",
        url: "/Purchase/GetProductList",
        datatype: "Json",
        data: {},
        success: function (data) {
            $('#ddlProductName').append('<option value="' + 0 + '">' + '' + '</option>');
            $.each(data, function (key, value) {
                $('#ddlProductName').append('<option value="' + value.ProductId + '">' + value.ProductName + '</option>');
            });
        }
    });
};


//======== Load Supplier dropdown list Open =============
SupplierMethod();
function SupplierMethod () {
    $('#ddlSupplier').empty();
    $.ajax({
        type: "POST",
        url: "/Purchase/GetSupplierList",
        datatype: "Json",
        data: {},
        success: function (data) {
            $('#ddlSupplier').append('<option value="' + 0 + '">' + '' + '</option>');
            $.each(data, function (key, value) {
                $('#ddlSupplier').append('<option value="' + value.SupplierId + '">' + value.SupplierName + '</option>');
            });
        }
    });
};






//======== Insert Product Method Open =============

$('#ddlProductName').select2({
    placeholder: 'Selete Product',
    language: {
        noResults: function () {
            return `<button style="width: 100%" type="button"
            class="btn btn-primary" 
            onclick="btnAddProduct()">+ Add New Item</button>
            </li>`;
        }
    },
    escapeMarkup: function (markup) { return markup;}
});
function btnAddProduct() {
    //var value = $(".select2-search__field").val();
    $('#ddlProductName').empty();
    $.ajax({
        url: '/Purchase/InsertProduct',
        type: "POST",
        data: "{ 'name': '" + $(".select2-search__field").val() + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //alert(data);
            $.each(data, function (key, value) {
                $('#ddlProductName').append('<option value="' + value.ProductId + '">' + value.ProductName + '</option>');
            });
        }
    });
    $('#ddlProductName').select2('close');
};

//======== Insert Product Method End =============



//======== Insert Product Model	 Method Open =============

$('#ddlProductModel').select2({
    placeholder: 'Selete Product Model',
    language: {
        noResults: function () {
            return `<button style="width: 100%" type="button"
            class="btn btn-primary" 
            onclick="btnAddProductModel()">+ Add New Item</button>
            </li>`;
        }
    },
    escapeMarkup: function (markup) { return markup; }
});
function btnAddProductModel() {
    $('#ddlProductModel').empty();
    var val1 = $('#ddlProductName').val();
    var val2 = $('.select2-search__field').val();

    $.ajax({
        type: "GET",
        url: '/Purchase/InsertProductModel',
        contentType: "application/json; charset=utf-8",
        data: { 'para1': val1, 'para2': val2 },

        dataType: "json",
        success: function (data) {
            $.each(data, function (key, value) {
                $('#ddlProductModel').append('<option value="' + value.ProductModelId + '">' + value.ModelName + '</option>');
            });
        }
        

    });
    $('#ddlProductModel').select2('close');
};

//======== Insert Product Method End =============




//======== Insert Supplier Method Open =============

$('#ddlSupplier').select2({
    placeholder: 'Selete Supplier',
    language: {
        noResults: function () {
            return `<button style="width: 100%" type="button"
            class="btn btn-primary" 
            onclick="btnAddSupplier()">+ Add New Supplier</button>
            </li>`;
        }
    },
    escapeMarkup: function (markup) { return markup; }
});
function btnAddSupplier() {
    //var value = $(".select2-search__field").val();
    $('#ddlSupplier').empty();
    $.ajax({
        url: '/Purchase/InsertSupplier',
        type: "POST",
        data: "{ 'name': '" + $(".select2-search__field").val() + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //alert(data);
            $.each(data, function (key, value) {
                $('#ddlSupplier').append('<option value="' + value.SupplierId + '">' + value.SupplierName + '</option>');
            });
        }
    });
    $('#ddlSupplier').select2('close');
};

//======== Insert Product Method End =============





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




//======== Edit Section Open =============

function EditPurchase(PurchaseId) {
    var url = "/Purchase/GetPurchaseRawMethod?PurchaseId=" + PurchaseId;
    $.ajax({
        type: "Get",
        url: url,
        success: function (data) {
            var obj = JSON.parse(data);
            
            $("#txtPurchaseId").val(obj.PurchaseId);
            $("#txtVoucherNo").val(obj.VoucherNo);
            $("#txtPurchaseDate").prop("type", "text");
            $("#txtPurchaseDate").val(obj.ConvertDate);

            $("#txtQuantity").val(obj.Quantity);
            $("#txtTotalPrice").val(obj.TotalPrice);
            $("#txtEmployeeId").val(obj.EmployeeId);
            $('#ImgFile').attr('src', obj.ImgPath);
            


            $("#ddlSupplier").select2("val", 0);
            $("#ddlProductModel").select2("val", 0);
            $("#ddlProductName").select2("val", 0);
            $("#btnAddProduct").val('Update');
            
        }

    })
}

//======== Edit Section End =============







//======== Delete Section Open  =============

var DeletePurchase = function (PurchaseId) {
    $("#DelPurchaseId").val(PurchaseId);
    $("#DeleteConfirmation").modal();
}


var ConfirmDelete = function () {
    var DelPurchaseId = $("#DelPurchaseId").val();
    $.ajax({
        type: "POST",
        url: "/Purchase/DeletePurchase?PurchaseId=" + DelPurchaseId,
        success: function (result) {
            //PurchaseTableDataMethod();
            $("#PurchaseTableShowlink")[0].click();
            $("#DeleteConfirmation").modal("hide");
        }
    })
}

//======== Delete Section End  =============





//======== Details Section Open  =============

var DetailsPurchase = function (PurchaseId) {
    $("#DetailsPurchaseId").val(PurchaseId);
    $('#DetailsData').empty();
    $.ajax({
        type: "post",
        url: "/Purchase/GetPurchaseRawMethod?PurchaseId=" + PurchaseId,
        datatype: "json",
        data: {},
        success: function (data) {
            var obj = JSON.parse(data);
            $('#DetailsData').append(

                "<tr><td rowspan='8' width='270'> <img class='productimg' src= '" + obj.ImgPath + "' ID='ImageID' height='250' width='250'> </td> <td> Purchase Id</td><td>" + obj.PurchaseId + "</td> </tr>" +
                "<tr> <td> Voucher No</td> <td>" + obj.VoucherNo + "</td></tr>" +
                "<tr><td> Product Name </td><td>" + obj.ProductName + "</td></tr>" +
                "<tr><td> Purchase Date </td><td>" + obj.ConvertDate + "</td></tr>" +
                "<tr><td> Supplier Name </td><td>" + obj.SupplierName + "</td></tr>" +
                "<tr><td> Quantity</td><td>" + obj.Quantity + "</td></tr>" +
                "<tr><td> Total Price</td><td>" + obj.TotalPrice + "</td></tr>" +
                "<tr><td width='120'> Employee Name</td><td>" + obj.EmpName + "</td></tr>" 
                
            );


        }
    });
    $("#DetailsModalSection").modal();



}

//======== Delete Section End  =============




//======== Insert Product Successfully Massage =============
var getresult = function (data) {

    $("#PurchaseTableShowlink")[0].click();
    //PurchaseTableDataMethod();
    //alert(data.result);
    $("#txtPurchaseDate").prop("type", "date");
    $('#txtVoucherNo').val('');
    $('#txtPurchaseDate').val('');

    $("#ddlSupplier").select2("val", 0);
    $("#ddlProductModel").select2("val", 0);
    $("#ddlProductName").select2("val", 0);


    $('#txtQuantity').val('');
    $('#txtTotalPrice').val('');
    $('#upimage').val('');
    $('#ImgFile').attr('src', '/images/App_images/noimageavailable.jpg');
    $("#txtPurchaseId").val(0);
    $("#btnAddProduct").val('Insert');

    var MassageShow = data.result;
    if (MassageShow == 'Insert') {
        insertMessageShow();
        MassageShow = "";

    }
    else if (MassageShow == 'Update') {
        updateMessageShow();
        MassageShow = "";
        
    }


};



var SearchOnSuccess = function (data) {
    $('#PurchaseCategory').val('');
    $('#txtboxSearch').val('');
};





function insertMessageShow() {
    document.getElementById("messagebar").className = 'messagebar messagebarinsert';
    document.getElementById("messagebar").innerHTML = "<h4>Data Inserted Successfully!!!</h4>";
    abcAsync();

}

function updateMessageShow() {
    document.getElementById("messagebar").className = 'messagebar messagebarupdate';
    document.getElementById("messagebar").innerHTML = "<h4>Data Updated Successfully!!!</h4>";
    abcAsync();

}

function deleteMessageShow() {
    document.getElementById("messagebar").className = 'messagebar messagebardelete';
    document.getElementById("messagebar").innerHTML = "<h4>Data Deleted Successfully!!!</h4>";
    abcAsync();

}





function abcAsync() {
    var promise = timeOutAsync(4000);
    promise.done(function () {
        document.getElementById("messagebar").className = 'messagebar';
    });
    return promise;
}

function timeOutAsync(ms) {
    var deferred = $.Deferred();
    setTimeout(function () { deferred.resolve() }, ms);
    return deferred.promise();
}
