$(document).ready(function(){
  $(".nav-tabs a").click(function(){
    $(this).tab('show');
  });
});

$(document).ready(function(){
    $("#myInput").on("keyup", function() {
    var value = $(this).val().toLowerCase();
    $("#ContentPlaceHolder1_GridView1 tr").filter(function() {
      $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
$("#ContentPlaceHolder1_GridView1 tr").eq(0).show()
    });
  });
});

$(document).ready(function(){
    $("#myInput2").on("keyup", function() {
    var value = $(this).val().toLowerCase();
    $("#ContentPlaceHolder1_Grid tr").filter(function() {
      $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
$("#ContentPlaceHolder1_Grid tr").eq(0).show()
    });
  });
});


$('.toast').toast('show');
