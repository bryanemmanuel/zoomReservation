

var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {

            "url": "/Admin/Schedule/GetAll",
            responsive: true,
        },

        order: [[1, 'desc']],


        "columns": [
            { "data": "date" },
            { "data": "availability" },




            {
                "data": "id",
                responsive: true,
                "render": function (data, type, row) {
                    var isLive = row.live;
                    var isArchive = row.archive;
                    return `
                     
                         <div class="row m-0 d-flex justify-content-center">
                               <div class="col-lg-auto col-md-auto col-sm-12 mt-1 d-flex justify-content-center"  >
                                    <a href="/Admin/Carousel/Upsert?id=${data}"
                                    class="btn btn-primary "> <i class="bi bi-pencil-square"></i> </a>
                                </div>
                                    
                                <div class="col-lg-auto col-md-auto col-sm-12 mt-1 d-flex justify-content-center" >
                                    <a href="/Admin/Carousel/ViewDetailsCarousel?id=${data}"
                                    class="btn btn-warning "> <i class="bi bi-eye"></i> </a>
                                </div>


                                <div class="col-lg-auto col-md-auto col-sm-12 mt-1 d-flex justify-content-center" >
                                  <a onClick=Delete('/Admin/Carousel/Delete/${data}')
                                  class="btn btn-danger"> <i class="bi bi-trash-fill"></i> </a>
                                </div>

                                 <div class="col-lg-auto col-md-auto col-sm-12 mt-1 d-flex justify-content-center"  >
                                  <a onClick=Live('/Admin/Carousel/Live/${data}')
                                  id="/Admin/Carousel/Live/${data}" class="btn ${isLive ? 'btn-success' : 'btn-danger'}"> <i class="bi bi-broadcast"></i> </a>
                                </div>

                                <div class="col-lg-auto col-md-auto col-sm-12 mt-1 d-flex justify-content-center"  >
                                  <a onClick=Archive('/Admin/Carousel/Archive/${data}')
                                  id="/Admin/Carousel/Archive/${data}" class="btn ${isArchive ? 'btn-success' : 'btn-danger'}"> <i class="bi bi-archive"></i> </a>
                                </div>


                      

                         </div>
					   
                        `
                }

            }
        ]
    });
}









function Delete(url) {


    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            swalWithBootstrapButtons.fire(

                $.ajax({
                    url: url,
                    type: 'DELETE',
                    success: function (data) {
                        if (data.success) {


                            dataTable.ajax.reload();


                        }

                        Swal.fire(
                            'Deleted!',
                            'Your file has been deleted.',
                            'success'
                        )


                    }
                })


            )
        } else if (

            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Your imaginary file is safe :)',
                'error'
            )
        }
    })

}


//---------------------------------------------
function Live(url) {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    var getId2 = document.getElementById(url);
    var getClass = getId2.className;
    if (getId2.classList.contains('btn-danger')) {
        swalWithBootstrapButtons.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Set To Live',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        })

            .then((result) => {
                if (result.isConfirmed) {
                    swalWithBootstrapButtons.fire(

                        $.ajax({
                            url: url,
                            type: 'POST',
                            success: function (data, type, row) {

                                dataTable.ajax.reload();
                                if (data.isLive) {
                                    Swal.fire(

                                        'Now Live!',
                                        'Your file has been in Production server.',
                                        'success'
                                    )
                                }
                                else {
                                    Swal.fire(
                                        'Not Live!',
                                        'This post will no longer be shown in production ',
                                        'success'
                                    )
                                }




                            }
                        })


                    )
                } else if (

                    result.dismiss === Swal.DismissReason.cancel
                ) {
                    swalWithBootstrapButtons.fire(
                        'Cancelled',
                        'Action has been cancelled',
                        'error'
                    )
                }
            })
    }
    else {
        swalWithBootstrapButtons.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Not To Live',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        })

            .then((result) => {
                if (result.isConfirmed) {
                    swalWithBootstrapButtons.fire(

                        $.ajax({
                            url: url,
                            type: 'POST',
                            success: function (data, type, row) {

                                dataTable.ajax.reload();
                                if (data.isLive) {
                                    Swal.fire(

                                        'Now Live!',
                                        'Your file has been in Production server.',
                                        'success'
                                    )
                                }
                                else {
                                    Swal.fire(
                                        'Not Live!',
                                        'This post will no longer be shown in production ',
                                        'success'
                                    )
                                }




                            }
                        })


                    )
                } else if (

                    result.dismiss === Swal.DismissReason.cancel
                ) {
                    swalWithBootstrapButtons.fire(
                        'Cancelled',
                        'Action has been cancelled',
                        'error'
                    )
                }
            })
    }
}

//---------------------------------------------
function Archive(url) {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    var getId2 = document.getElementById(url);
    var getClass = getId2.className;
    if (getId2.classList.contains('btn-danger')) {
        swalWithBootstrapButtons.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Add to Archive',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        })

            .then((result) => {
                if (result.isConfirmed) {
                    swalWithBootstrapButtons.fire(

                        $.ajax({
                            url: url,
                            type: 'POST',
                            success: function (data, type, row) {

                                dataTable.ajax.reload();
                                if (data.isLive) {
                                    Swal.fire(
                                        'Item remove from Archive!',
                                        'This post will show in test ',
                                        'success'

                                    )
                                }
                                else {
                                    Swal.fire(
                                        'Now in Archive!',
                                        'This post will no longer be shown in test and production',
                                        'success'
                                    )
                                }




                            }
                        })


                    )
                } else if (

                    result.dismiss === Swal.DismissReason.cancel
                ) {
                    swalWithBootstrapButtons.fire(
                        'Cancelled',
                        'Action has been cancelled',
                        'error'
                    )
                }
            })
    }
    else {
        swalWithBootstrapButtons.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Remove from Archive',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        })

            .then((result) => {
                if (result.isConfirmed) {
                    swalWithBootstrapButtons.fire(

                        $.ajax({
                            url: url,
                            type: 'POST',
                            success: function (data, type, row) {

                                dataTable.ajax.reload();
                                if (data.isLive) {
                                    Swal.fire(

                                        'Now in Archive!',
                                        'This post will no longer be shown in test and production',
                                        'success'
                                    )
                                }
                                else {
                                    Swal.fire(
                                        'Item remove from Archive!',
                                        'This post will show in test ',
                                        'success'
                                    )
                                }




                            }
                        })


                    )
                } else if (

                    result.dismiss === Swal.DismissReason.cancel
                ) {
                    swalWithBootstrapButtons.fire(
                        'Cancelled',
                        'Action has been cancelled',
                        'error'
                    )
                }
            })
    }
}





    //var schedules = _unitOfWork.Schedule.GetAll().Where(schedule => schedule.availability == "available").ToList();

    //var data = schedules.Select(row => new
    //    {
    //        id = row.Id,
    //        title = "AVAILABLE",
    //        start = row.date,
    //        end = row.date
    //    });

    //return Json(data);


