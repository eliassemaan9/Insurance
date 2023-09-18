var apiURL ='https://localhost:44327/';
var insuredID = 0;

function bindHospitals() {
    var Url = apiURL + "api/Hospital/GetHospitals";
    $.ajax({
        url: Url,
        type: "GET",
        async: false,
        success: function (result) {
            var html = '<option value="null">--- Select ---</option>';
            $.map(result, function (val, i) {
                html += '<option value="' + val.id + '">' + val.name + '</option>';
            });
            $('#hospital').html(html);
        },
    });
}

function bindTreatings() {
    var Url = apiURL + "api/Treating/GetTreating";
    $.ajax({
        url: Url,
        type: "GET",
        async: false,
        success: function (result) {
            var html = '<option value="null">--- Select ---</option>';
            $.map(result, function (val, i) {
                html += '<option value="' + val.id + '">' + val.fullName + '</option>';
            });
            $('#treating').html(html);
        },
    });
}
function bindStatus() {
    var Url = apiURL + "api/Lookups/GetLookupByParentCode?code=STTS";
    $.ajax({
        url: Url,
        type: "GET",
        async: false,
        success: function (result) {
            var html = '';
            $.map(result, function (val, i) {
                html += `<input type="radio" name="status" id="${val.id}" value="${val.id}" class="mt-1 me-2" /> <label for="${val.id}" class="form-label me-3">${val.name}</label>`;
            })
         
            $('#status').html(html);
        },
    });
}

function bindGender() {
    var Url = apiURL + "api/Lookups/GetLookupByParentCode?code=GND";
    $.ajax({
        url: Url,
        type: "GET",
        async: false,
        success: function (result) {
            var html = '';
            $.map(result, function (val, i) {
             
                html += `<input type="radio" name="gender" value="${val.name}" id="${val.name}" class="mt-1 me-2" disabled /> <label for="${val.id}" class="form-label me-3">${val.name}</label>`;
       
            })
            $('#gender').html(html);
        },
    });
}


function bindInsuredData() {
    var insuredCardNbr = $("#insured-card-number").val();
    var Url = apiURL + "api/Insured/GetInsuredByCardNumber?CardNumber=" + insuredCardNbr;
    $.ajax({
        url: Url,
        type: "GET",
        async: false,
        success: function (response) {
          
            var insuredName = response.name;
            var dob = response.dob;
            var gender = response.gender.name;
            insuredID = response.id;
            $("#insured-name").val(insuredName);
            $("#dob").val(formatDate(dob, 'dd/MM/yyyy'));
            gender === "Male" ? $("#Male").prop("checked", true) : $("#Female").prop("checked", false);
        },
    });
}

$(document).ready(function() {
  if(localStorage.getItem("user") == "")
  {
     window.open("login.html", "_self");
  }
     bindHospitals();
     bindStatus();
     bindGender();
     bindTreatings();
    $("#hospital").select2();
    $("#treating").select2();
    $("#btn-save").click(function() {
        var id = 0;
        var hospital = $("#hospital").val();
        var admissionDate = $("#admission").val();
        var medicalCase = $("#medical-case").val();
        var estimatedCost = $("#estimated-cost").val();
        var status = $("input[type='radio'][name='status']:checked").val();
        var remarks = $("#remarks").val();
        var treating = $("#treating").val();
        var Url = apiURL + "api/ClaimCenter/SaveClaimCenter";

        if(hospital === '' || admissionDate === '' || treating === '' || medicalCase === '' || estimatedCost === '' || status === '') {
            alert("Please fill all requied Field");
        } else {
          
            var claimCenter = new Object();
            claimCenter.id = 0;
            claimCenter.insuredId = insuredID;
            claimCenter.hospitalId = hospital;
            claimCenter.admissionDate = admissionDate;
            claimCenter.medicalCase = medicalCase;
            claimCenter.estimatedCost = estimatedCost;
            claimCenter.treatingId = treating;
            claimCenter.statusId = status;
            claimCenter.remarks = remarks;
            claimCenter.isDeleted = false;
         
            $.ajax({
                url: Url,
                type: "POST",
                async: false,
                contentType: 'application/json',
                data: JSON.stringify(claimCenter),
                success: function (result) {
                    if(result.success) {
                        alert("Claim Center Created successfuly")
                        window.open("search.html", "_self");
                    } else {
                      
                    }
                },
            });
        }
    });
});



var formatDate = function (date, mask) {
    if (date == null) {
      return;
    }
    var EnglishMonths = [
      "Jan",
      "Feb",
      "Mar",
      "Apr",
      "May",
      "June",
      "July",
      "Aug",
      "Sept",
      "Oct",
      "Nov",
      "Dec",
    ];
    var ArabicMonths = [
      "يناير",
      "شباط",
      "مارس",
      "ابريل",
      "مايو",
      "يونيو",
      "يوليو",
      "اغسطس",
      "سبتمبر",
      "اكتوبر",
      "نوفمبر",
      "ديسمبر",
    ];
    var FormattedDate = "";
    if (date == "0001/01/01") date = new Date(0);
  
    // var NormalizedDate = NormalizeDate(date);
  
    var d = new Date(date),
      month = "" + (d.getMonth() + 1),
      day = "" + d.getDate(),
      year = d.getFullYear(),
      hour = d.getHours(),
      mins = d.getMinutes();
  
    if (month.length < 2) month = "0" + month;
    if (day.length < 2) day = "0" + day;
    if (hour.length < 2) hour = "0" + hour;
    if (mins.length < 2) mins = "0" + mins;
  
    mask = mask == null || mask == "" ? "dd/mm/yyyy" : mask;
    mask = mask.toLowerCase();
    if (mask == "dd/mmm/yyyy") {
      FormattedDate =
        $CurrentLanguage == "en"
          ? String(day) +
          ", " +
          String(EnglishMonths[month - 1]) +
          " " +
          String(year)
          : String(day) +
          ", " +
          String(ArabicMonths[month - 1]) +
          " " +
          String(year);
    } else if (mask == "dd/mmm/yyyy hh:mm") {
      FormattedDate =
        $CurrentLanguage == "en"
          ? String(day) +
          ", " +
          String(EnglishMonths[month - 1]) +
          " " +
          String(year) +
          " " +
          String(hour) +
          ":" +
          String(mins)
          : String(day) +
          ", " +
          String(ArabicMonths[month - 1]) +
          " " +
          String(year) +
          " " +
          String(hour) +
          ":" +
          String(mins);
    }
    if (mask == "yyyy/mm/dd")
      FormattedDate = String(year) + "/" + String(month) + "/" + String(day);
    else if (mask == "dd/mm/yyyy")
      FormattedDate = String(day) + "/" + String(month) + "/" + String(year);
    else if (mask == "mm/dd/yyyy")
      FormattedDate = String(month) + "/" + String(day) + "/" + String(year);
    else if (mask == "yyyy-mm-dd")
      FormattedDate = String(year) + "-" + String(month) + "-" + String(day);
    else if (mask == "ddmmyyyy")
      FormattedDate = String(day) + String(month) + year;
    else if (mask == "dd/mm/yyyy hh:mm") {
      hour = String(hour).length == 1 ? "0" + String(hour) : String(hour);
      mins = String(mins).length == 1 ? "0" + String(mins) : String(mins);
      FormattedDate =
        String(day) +
        "/" +
        String(month) +
        "/" +
        String(year) +
        "  " +
        String(hour) +
        ":" +
        String(mins);
    } else if (mask == "yyyy-mm-dd hh:mm") {
      hour = String(hour).length == 1 ? "0" + String(hour) : String(hour);
      mins = String(mins).length == 1 ? "0" + String(mins) : String(mins);
      FormattedDate =
        String(year) +
        "/" +
        String(month) +
        "/" +
        String(day) +
        " " +
        String(hour) +
        ":" +
        String(mins);
    } else if (mask == "hh:mm") {
      var TimeUnit;
      //var hour = (hour + 24 - 2) % 24;
      var mid = "AM";
      if (hour == 0) {
        //At 00 hours we need to show 12 am
        hour = 12;
      } else if (hour > 12) {
        hour = hour % 12;
        mid = "PM";
      }
  
      hour = String(hour).length == 1 ? "0" + String(hour) : String(hour);
      mins = String(mins).length == 1 ? "0" + String(mins) : String(mins);
      mid = $CurrentLanguage == "en" ? mid : mid == "AM" ? "ص" : "م";
      FormattedDate = String(hour) + ":" + String(mins) + " " + String(mid);
    } else if (mask == "dd/mm/yyyy hh:mm u") {
      var TimeUnit;
      //var hour = (hour + 24 - 2) % 24;
      var mid = "AM";
      if (hour == 0) {
        //At 00 hours we need to show 12 am
        hour = 12;
      } else if (hour > 12) {
        hour = hour % 12;
        mid = "PM";
      }
  
      hour = String(hour).length == 1 ? "0" + String(hour) : String(hour);
      mins = String(mins).length == 1 ? "0" + String(mins) : String(mins);
      mid = $CurrentLanguage == "en" ? mid : mid == "AM" ? "ص" : "م";
      FormattedDate =
        String(year) +
        "/" +
        String(month) +
        "/" +
        String(day) +
        " " +
        String(hour) +
        ":" +
        String(mins) +
        " " +
        String(mid);
    } else if (mask == "dd/mm hh:mm u") {
      var mid = "AM";
      if (hour == 0) {
        //At 00 hours we need to show 12 am
        hour = 12;
      } else if (hour > 12) {
        hour = hour % 12;
        mid = "PM";
      }
  
      hour = String(hour).length == 1 ? "0" + String(hour) : String(hour);
      mins = String(mins).length == 1 ? "0" + String(mins) : String(mins);
      mid = $CurrentLanguage == "en" ? mid : mid == "AM" ? "ص" : "م";
      FormattedDate =
        String(day) +
        "/" +
        String(month) +
        " " +
        String(hour) +
        ":" +
        String(mins) +
        " " +
        String(mid);
    }
    return FormattedDate;
  };

  function logoutUser() {
    localStorage.removeItem("user");
    window.localStorage.setItem("user","");
    window.open("login.html", "_self");
  }
  $.ajaxSetup({
    headers: {
         Authorization: 'Bearer ' + localStorage.getItem("user"),
        'Accept': 'application/json',
    },
    beforeSend: function (xhr) {
        xhr.setRequestHeader("Accept", "application/vvv.website+json;version=1");
    }
});