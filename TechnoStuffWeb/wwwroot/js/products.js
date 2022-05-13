﻿let dataTable

$(document).ready(function() {[
    loadDataTable()
]})

function loadDataTable() {
    dataTable = $('#productsTable').DataTable({
        'ajax': {
            'url': '/admin/product/getAll'
        },
        'columns': [
            { 'data': 'title', 'width': '15%' },
            { 'data': 'description', 'width': '15%' },
            { 'data': 'manufacturer', 'width': '15%' },
            { 'data': 'price', 'width': '15%' },
            { 'data': 'category.name', 'width': '15%' },
            { 
                'data': 'id',
                'render': function(data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/admin/product/upsert/${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-fill"></i>Edit 
                            </a>
                            <a onClick=destroy('/admin/product/destroy/${data}') class="btn btn-danger mx-2">
                                <i class="bi bi-trash3-fill"></i>Delete 
                            </a>
                        </div>
                    `
                }, 
                'width': '15%' 
            }
        ]
    })
}

function destroy(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
      }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function(data) {
                    if (data.success) {
                        dataTable.ajax.reload()
                        toastr.success(data.message)
                    } else {
                        toastr.error(data.message)
                    }
                }
            })
        }
      })
}