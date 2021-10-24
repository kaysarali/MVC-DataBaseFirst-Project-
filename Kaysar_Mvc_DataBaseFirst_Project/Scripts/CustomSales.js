

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