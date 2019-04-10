$(document).ready(function () {
    // Call Web API to get a list of Cats
    $.ajax({
        url: '/api/CatDetails/',
        type: 'GET',
        dataType: 'json',
        beforeSend: function () {
            $("#loadingDiv").toggle();
        },
        success: function (cats) {
            $("#loadingDiv").toggle();
            renderCats(cats);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });

});

//var validator
//    = new jQueryValidatorWrapper("inForm",
//        rules, messages);

function renderCats(cats) {
    // Iterate over the collection of data
    $.each(cats, function (index, cat) {
        // Add a row to the Cat table
        addCatRow(cat);
    });
}

function addCatRow(cat) {
    // Check if <tbody> tag exists, add one if not
    if ($("#catTable tbody").length == 0) {
        $("#catTable").append("<tbody></tbody>");
    }
    // Append row to <table>
    $("#catTable tbody").append(
        catBuildRow(cat));
}

function catBuildRow(cat) {
    var ret =
        "<tr>" +
        "<td>" + cat.CatId + "</td>" +
        "<td>" + cat.CatName + "</td>" +
        "<td>" + cat.CatDob + "</td>" +
        "<td>" + cat.CatGender + "</td>" +
        "<td>" + cat.CatBreed + "</td>" +
        "<td> <button type='button' onclick='getCatDetails(this);' class='btn btn - danger btn - rounded btn - sm my - 0' data-id='" + cat.CatId + "' data-toggle='modal' data-target='#catDetModal'><span class='glyphicon glyphicon-edit'></button> </td>" +
        "<td> <button type='button' class='btn btn - danger btn - rounded btn - sm my - 0' data-id='" + cat.CatId + "' onclick='deleteCat(this)'><span class='glyphicon glyphicon-remove'></button> </td>" +
        "</tr>";
    return ret;
}


function toggleModalWindow() {
    $('#catDetModal').modal('toggle')
}

function handleException(request, message,
    error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON != null) {
        msg += "Message" +
            request.responseJSON.Message + "\n";
    }
    alert(msg);
}

var Cat = {
    CatId: 0,
    CatName: "",
    CatAge: "",
    CatGender: "",
    CatDob: "",
    CatBreed: ""
}


function getCatDetails(ctl) {
    // get catId from the data attribute of button control
    var id = $(ctl).data("id");

    // Call Web API to get a details of Cat
    $.ajax({
        url: "/api/CatDetails/" + id,
        type: 'GET',
        dataType: 'json',
        success: function (cat) {
            mapCatDetailsToFields(cat);

            // Change Update Button Text
            $("#updateButton").text("Update");
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function mapCatDetailsToFields(cat) {
    $("#catid").val(cat.CatId);
    $("#catname").val(cat.CatName);
    $("#catdob").val(cat.CatDob);
    $("#catgender").val(cat.CatGender);
    $("#catbreed").val(cat.CatBreed);
}

function modalUpdateClick() {
    if (! $("#inForm").valid()) {
        console.log('Input Not Valid');
        return;
    }
    else {
        console.log('All inputs validated');
    }
 

    Cat = new Object();
    Cat.CatId = $("#catid").val();
    Cat.CatName = $("#catname").val();
    Cat.CatDob = $("#catdob").val();
    Cat.CatGender = $("#catgender").val();
    Cat.CatBreed = $("#catbreed").val();

    if ($("#updateButton").text().trim() ==
        "Add") {
        addNewCatDetails(Cat);
    }
    else {
        updateCatDetails(Cat);
        //updateCatInTable(Cat);
    }
}

function updateCatDetails(cat) {
    $.ajax({
        url: "/api/CatDetails",
        type: 'PUT',
        contentType:
            "application/json;charset=utf-8",
        data: JSON.stringify(cat),
        success: function (cat) {
            updateCatInTable(cat);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function addNewCatDetails(cat) {
    console.log("inside addNewCatDetails");
    resetModalForm();
    $.ajax({
        url: "/api/CatDetails",
        type: 'POST',
        contentType:
            "application/json;charset=utf-8",
        data: JSON.stringify(cat),
        success: function (cat) {
            updateCatInTable(cat);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}


function updateCatInTable(cat) {
    console.log("Inside updateCatInTable");
    var row = $("#catTable button[data-id='" +
        cat.CatId + "']").parents("tr")[0];
    
    //$(row).after(addCatRow(cat));
    $(row).before(addCatRow(cat));
    $(row).remove();
    resetModalForm();

    toggleModalWindow();
}

function resetModalForm() {
    $("#catname").val("");
    $("#catdob").val("");
    $("#catgender").val("");
    $("#catbreed").val("");
    $("#updateButton").text("Add");
}

function deleteCat(ctl) {
    var id = $(ctl).data("id");

    $.ajax({
        url: "/api/CatDetails/" + id,
        type: 'DELETE',
        success: function (cat) {
            $(ctl).parents("tr").remove();
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}


        //$('#catDetModal').on('hidden.bs.modal', function () {
        //    $(this).find('#catDetModal').trigger('reset');
        //})