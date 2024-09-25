let dataTable;

$(document).ready(function () {
    fillDataTable();
});

function fillDataTable() {
    dataTable = $('#tlBodegasData').DataTable({

        "ajax":{
            "url": "/Admin/Bodega/GetBodegas"
        },
        "columns": [
            {"data":"nombre","width":"20%"},
            {"data":"descripcion","width":"40%"},
            {
                "data":"estado",
                "render" : function(data){
                    if(data==true){
                        return "Activo";
                    }
                    else{
                        return "Inactivo";
                    }
                }, "width":"20%"
            },
            {
                "data":"id",
                "render": function(data){
                    return  `<div class="text-center">
                        <a href="/admin/Bodega/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                        <i class="bi bi-pencil-square"></i>
                        </a>
                        <a onclick=Delete("/Admin/Bodega/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                           <i class="bi bi-trash3-fill"></i>
                        </a>                    
                    </div>
                    `;
                }, "width":"20%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Esta seguro de Eliminar la Bodega?",
        text: "Despues de Eliminar la Bodega no se podra recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((borrar) => {
        if (borrar) {
            debugger;
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
