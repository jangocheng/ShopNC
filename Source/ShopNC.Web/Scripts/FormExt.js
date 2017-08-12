function getFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
};

//把表单对话框关闭，然后 重新加载一下表格的数据
function addSuccess(data) {
    data.Type = "B";
    parent.reLoad(data);

}

//修改之后调用次方法
function editeSuccess(data) {
    parent.reLoad(data);
}


function reLoad(data) {
    var title = "结果";
    switch (data.Type) {
        case "A": title = "添加结果"; break;
        case "B": title = "修改结果"; break;
        case "C": title = "删除结果"; break;
        default:
    }
    title = (data.Title == undefined || data.Title.length <= 0) ? title : data.Title;
    if (data.Code == 1) {
        $('#divEdite').dialog("close");
        $('#divEdite').remove();
        $('#' + gridId).datagrid('reload');
    }
    showResult(title, data);
}

//删除成功
function deleteSuccess(data) {
    $('#' + gridId).datagrid('reload');
    showResult('删除成功',data);
}

function showResult(title, data) {
    if (data.Url) {
        window.top.location = data.Url;
    }
    else {
        $.messager.alert(title, (data.Message==undefined?"成功":data.Message));
    }
}

var $btn;
function ajaxFromBegin() {
    $btn = $("#ajaxFromBegin input[type='submit']");
    $btn.val("提交中..").attr("disabled", "disabled");
}