﻿var gridId = null;
function Grid() {
    this.InitList = function (option) { initList(option);} 
}

function initList(option) {
    if (isNull(option.url)||isNull(option.columns)) {
        return "传入参数有误";
    }
      gridId = isNull(option.gridId) ? "tt" : option.gridId;
      var grid = $('#'+gridId).datagrid({
            url:option.url,
            title:isNull(option.title)? '列表':option.title,
            width: 'auto',
            height: '300',
            fitColumns: true,
            idField: isNull(option.idField) ? 'ID' : option.idField,
            loadMsg: '正在加载...',
            pagination: true,
            singleSelect: false,
            pageSize: 10,
            pageNumber: 1,
            pageList: [10, 20, 30],
            queryParams: option.queryParams,
            columns: [option.columns],
            toolbar:isNull(option.toolbar)?[]:option.toolbar
      });
      grid.resizeDataGrid(0, 0, 300, 600);
}


//修改 弹出层 通过iframe指向新页面
function dealEditeSingle(url,dialogTitle,tip) {
    var rows = $("#"+gridId).datagrid('getSelections');
    if (rows.length != 1) {
        tip=isNull(tip)?"选择一条记录信息修改":tip;
        $.messager.alert("提示信息", tip);
    }
    else {
        for (i = 0; i < rows.length; i++) {
            // alert(rows[i].ID);
            var divEdite = '<div id="divEdite"><iframe id="ifrmEdite" src="' +url + rows[0].ID + '" scrolling="no" frameborder="0" width="100%" height="100%">';
            divEdite += '</iframe></div>';
            $(divEdite).dialog({
                title: isNull(dialogTitle) ? '修改' : dialogTitle,
                width: 'auto',
                height: 'auto',
                closed: false,
                cache: false,
                modal: true,
                buttons: []
            });

        }
    }
}

//批量处理 弹出层 通过iframe指向新页面
function dealEditeMore(url, dialogTitle, tip) {
    var rows = $("#" + gridId).datagrid('getSelections');
    var ids = "";
    if (rows.length <= 0) {
        tip = isNull(tip) ? "至少选择一条记录信息" : tip;
        $.messager.alert("消息提示", tip);
    }
    else {
        for (var i = 0; i < rows.length; i++) {
            ids += rows[i].ID + ",";
        }
        ids = ids.substring(0, ids.length - 1);
        for (i = 0; i < rows.length; i++) {
            // alert(rows[i].ID);
            var divEdite = '<div id="divEdite"><iframe id="ifrmEdite" src="' + url + rows[0].ID + '" scrolling="no" frameborder="0" width="100%" height="100%">';
            divEdite += '</iframe></div>';
            $(divEdite).dialog({
                title: isNull(dialogTitle) ? '修改' : dialogTitle,
                width: 'auto',
                height: 'auto',
                closed: false,
                cache: false,
                modal: true,
                buttons: []
            });

        }
    }
}


//处理删除

function dealDelete() {
    var rows = $("#tt").datagrid('getSelections');
    var ids = "";
    if (rows.length <= 0) {
        $.messager.alert("消息提示", "选择信息进行删除");
    }
    else {
        $.messager.confirm("确定提示", "确定要删除信息吗?", function (r) {
            if (!r) {
                return false;
            }
            for (var i = 0; i < rows.length; i++) {
                ids += rows[i].ID + ",";
            }
            ids = ids.substring(0, ids.length - 1);
            $.post("/UserInfo/Delete", { "ids": ids }, function (data) {
                //删除成功 取消选中项
                $('#tt').datagrid("clearSelections");

                //重新加载数据表格
                LoadInitData();
            })

        })

    }

}


//检查是否为null or undefined
function  isNull(value) {
    if (value==undefined||value==null||value.length<=0) {
        return true;
    }
    
    return false;
}

//把表单对话框关闭，然后 重新加载一下表格的数据
function affterAdd() {
    $("#addDialog").dialog("close");
    LoadInitData();

}
//修改用户之后调用次方法
function aterUpdateEdit() {
    $('#divEdite').dialog("close");
    $('#'+gridId).datagrid('reload');
}