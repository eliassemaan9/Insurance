var _status = [];
var table;
var savedId = 0;
var insuredId = 0;
var admiDate = new Date();
function updateInfo() {
    $("#update-modal").modal("show");
  }
  var apiURL ='https://localhost:44327/';


  function bindHospitals() {
    var Url = apiURL + "api/Hospital/GetHospitals";
    $.ajax({
        url: Url,
        type: "GET",
        async: false,
        success: function (result) {
            var html = '<option value="">--- Select ---</option>';
            $.map(result, function (val, i) {
                html += '<option value="' + val.id + '">' + val.name + '</option>';
            });
            $('#hospital').html(html);
          
        },
    });
}
function bindModalHospitals() {
    var Url = apiURL + "api/Hospital/GetHospitals";
    $.ajax({
        url: Url,
        type: "GET",
        async: false,
        success: function (result) {
            var html = '';
            $.map(result, function (val, i) {
                html += '<option value="' + val.id + '">' + val.name + '</option>';
            });
         
            $('#ddlModalhospital').html(html);
        },
    });
}


  $(document).ready(function(){
        bindModalHospitals();
        bindHospitals();
        bindStatus();
        bindGender();
        bindTreatings();
        bindClaimCenter();   
        
     if(localStorage.getItem("user") == "")
     {
        window.open("login.html", "_self");
     }
    //  var table = $('#datatable').DataTable(); $('#datatable tbody').on( 'dblclick', 'tr', function ()
    //   { var id = table.row( this ).data(); alert("0"); } );

      var table = $('#datatable').DataTable(); 
     


});

$("#btn-search").click(function() {
     
 bindClaimCenter();      
});
$.ajaxSetup({
    headers: {
         Authorization: 'Bearer ' + localStorage.getItem("user"),
        'Accept': 'application/json',
    },
    beforeSend: function (xhr) {
        xhr.setRequestHeader("Accept", "application/vvv.website+json;version=1");
    }
});

function bindClaimCenter()
{
    
    var fromDate = $("#from-date").val();
    var toDate = $("#to-date").val();
    var cardNbr = $("#card-number").val();
    var hospital = $("#hospital").val();
    var data = {
        fromDate: fromDate,
        toDate: toDate,
        cardNbr: cardNbr,
        hospital: hospital
    }

    var Url = apiURL + "api/ClaimCenter/GetClaimCenterData?fromDate=" + fromDate + "&toDate=" + toDate + "&cardNumber=" + cardNbr+"&hospitalId="+hospital;
    $.ajax({
        url: Url,
        type: "GET",
        success: function (response)
        { 
            var data = response;
          
            table = $('#datatable').DataTable({
                responsive: true,
                destroy:true,
                dom: 'Bfrtip',
                buttons: [
                    'pageLength', 'excel', 'pdf', 'print'
                ],
                "data": data,
                columns: [
                    {
                
                        'data': null,
                        render: function (data, type, row) {
                        
                            return row.id;
                        },
                        "visible": false,
                    },
                    {
                        'data': null,
                        render: function (data, type, row) {
                            return formatDate( row.admissionDate);
                        },
                    },
                    {
                        'data': null,
                        render: function (data, type, row) {
                          return   `<a class="text-dark fw-bolder text-hover-primary" href="#"  data-bs-toggle="modal" data-bs-target="#update-modal"
                        onclick="getClaimCenterbyId('`+ row.id + `');return false;">` + row.cardNumber  + `</a>`
                            ;
                        },
                    },
                    {
                        'data': null,
                        render: function (data, type, row) {
                            return row.insuredName;
                        },
                    },
                    {
                        'data': null,
                        render: function (data, type, row) {
                            return row.hospital;
                        },
                    },
                    {
                        'data': null,
                        render: function (data, type, row) {
                            return row.medicalCase;
                        },
                    },
                    {
                        'data': null,
                        render: function (data, type, row) {
                            return row.estimatedCost;
                        },
                    },
                    {
                        'data': null,
                        render: function (data, type, row) {
                            return row.status;
                        },
                    },
                ],
                initComplete: function () {
                    var btns_excel = $('.buttons-excel');
                    btns_excel.addClass('btn btn-success');
                    btns_excel.removeClass('dt-button');
                    btns_excel.removeClass('buttons-html5');
                    btns_excel.removeClass('buttons-excel');

                    var btns_pdf = $('.buttons-pdf');
                    btns_pdf.addClass('btn btn-danger');
                    btns_pdf.removeClass('dt-button');
                    btns_pdf.removeClass('buttons-html5');
                    btns_pdf.removeClass('buttons-pdf');

                    var btns_print = $('.buttons-print');
                    btns_print.addClass('btn btn-primary');
                    btns_print.removeClass('dt-button');
                    btns_print.removeClass('buttons-html5');
                    btns_print.removeClass('buttons-print');

                    var btns_collection = $('.buttons-collection');
                    btns_collection.addClass('btn btn-dark');
                    btns_collection.removeClass('dt-button');
                    btns_collection.removeClass('buttons-html5');
                    btns_collection.removeClass('buttons-collection');
                }
            });
        },
        
    });
}
function logoutUser() {
    localStorage.removeItem("user");
    window.localStorage.setItem("user","");
    window.open("login.html", "_self");
  }
  function getClaimCenterbyId(id) {
   
    var Url = apiURL + "api/ClaimCenter/GetClaimCenterById?id=" + id;
    $.ajax({
        url: Url,
        type: "GET",
        async: false,
        success: function (response) {
            savedId= response.id
            admiDate = response.admissionDate;
           // var gender = response.insured.gender.name;
            $("#insured-card-number").val(response.insured.cardNumber);
            bindInsuredData(response.insured.cardNumber);
         
            $("#admission").val(formatDate(response.admissionDate,'mm/dd/yyyy'));
            $("#medical-case").val(response.medicalCase);
            $("#estimated-cost").val(response.estimatedCost);
            $("#remark").val(response.remarks);
          
            $("#treating").val(response.treating.id);
            $("#ddlModalhospital").val(response.hospital.id);
            // if (result.vat == true)
            // $('#checkVat').attr('checked', 'checked');
            var coveredstatus= getLookupByCode('STTS_C', _status).id;
            var notCoveredstatus= getLookupByCode('STTS_NC', _status).id;
            var pendingstatus= getLookupByCode('STTS_P', _status).id;

            if(response.status.name == "Covered")
            {
              $("#"+coveredstatus).prop("checked", true);
            }
            else  if(response.status.name == "Not Covered")
            {
              $("#"+notCoveredstatus).prop("checked", true);
            }
            else
            {
              $("#"+pendingstatus).prop("checked", true);
            }
         //   $("[name='status']").val(response.status.id);
            // $("#dob").val(formatDate(response.insured.dob, 'dd/MM/yyyy'));
            // gender === "Male" ? $("#Male").prop("checked", true) : $("#Female").prop("checked", false);
            // $("#insured-name").val(response.insured.name);
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
          _status = result;
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


function bindInsuredData(insuredCardNbr) {
  
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


function getLookupByCode(code, arr) {
  var _item = $.grep(arr, function (e) {
    return e.lookupCode == code;
  });
  return _item[0];
}



$("#btn_save").click(function() {
  var id = savedId;
  alert(savedId)
  var hospital = $("#ddlModalhospital").val();
  var medicalCase = $("#medical-case").val();
  var estimatedCost = $("#estimated-cost").val();
  var status = $("input[type='radio'][name='status']:checked").val();
  var remarks = $("#remark").val();
  var treating = $("#treating").val();
  var Url = apiURL + "api/ClaimCenter/SaveClaimCenter";



  if(hospital === '' || treating === '' || medicalCase === '' || estimatedCost === '' || status === '') {
      alert("Please fill all requied Field");
  } else {
    
      var claimCenter = new Object();
      claimCenter.id = id;
      claimCenter.insuredId = insuredID;
      claimCenter.hospitalId = hospital;
      claimCenter.admissionDate = admiDate
      claimCenter.medicalCase = medicalCase;
      claimCenter.estimatedCost = estimatedCost;
      claimCenter.treatingId = treating;
      claimCenter.statusId = status;
      claimCenter.remarks = remarks;
      claimCenter.isDeleted = false;
      // insuredInfo = {
      //     insuredId:insuredID,
      //     hospitalId: hospital,
      //     admissionDate: admissionDate,
      //     medicalCase: medicalCase,
      //     estimatedCost: estimatedCost,
      //     treatingId : treating,
      //     statusId: status,
      //     remarks: remarks,
      //     isDeleted: false
      // }
      $.ajax({
          url: Url,
          type: "POST",
          async: false,
          contentType: 'application/json',
          data: JSON.stringify(claimCenter),
          success: function (result) {
              if(result.success) {
                  alert("Claim Center Updated successfuly")
                  bindClaimCenter();   
              } else {
                
              }
          },
      });
  }
});