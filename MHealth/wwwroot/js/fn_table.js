function add_row(tb) {
    
    var $tableBody = $('#' + tb).find("tbody"),
        $trLast = $tableBody.find("tr:last"),
        $trNew = $trLast.clone();

    var row_count = $trLast.index() + 1;
    $trNew.find('input,select').each(function () {
        var name = this.name;
        this.name = name.substring(0, name.length - 1) + (row_count + 1);


        var id = this.id;
        this.id = id.substring(0, name.length - 1) + (row_count + 1);

    });

    //reset the project selection
    $trNew.find('select').each(function () {

        this.selectedIndex = 0;

    });



    $trNew.find('input').each(function () {

        $(this).val("");
        //$(this).attr("disabled", "disabled");
    });

    $trLast.after($trNew);


}

function delete_row(row, tb) {
    var rows = $("#" + tb + " tbody tr").length;
    if (rows > 1) {
        $(row).parent().parent().remove();
    } else {
        clear_row(row);
    }
}

function clear_row(row) {
    $(row).parent().parent().find("input").each(function () {
        $(this).val("");
    });



    $(row).parent().parent().find("select").each(function () {
        this.selectedIndex = 0;
    });

}