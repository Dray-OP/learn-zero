﻿(function () {
    $(function () {
        var _table = $('#DynamicEntityPropertiesTable');
        var _dynamicEntityPropertyAppService = abp.services.app.dynamicEntityProperty;
        var _initialized = false;
        var _selectAnEntity = new app.ModalManager({
            viewUrl: abp.appPath + 'Accounting/DynamicProperty/SelectAnEntityModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Accounting/Views/DynamicProperties/_SelectAnEntityModal.js',
            modalClass: 'SelectAnEntityForDynamicPropertyModal',
            cssClass: 'scrollable-modal'
        });

        var _permissions = {
            edit: abp.auth.hasPermission('Pages.Administration.DynamicEntityPropertyValue.Edit')
        };

        function initializeTable() {
            if (_initialized) {
                return;
            }
            _initialized = true;

            _table.DataTable({
                paging: false,
                serverSide: false,
                processing: false,
                listAction: {
                    ajaxFunction: _dynamicEntityPropertyAppService.getAllEntitiesHasDynamicProperty
                },
                columnDefs: [
                    {
                        className: 'control responsive',
                        orderable: false,
                        render: function () {
                            return '';
                        },
                        targets: 0
                    },
                    {
                        targets: 1,
                        data: null,
                        orderable: false,
                        defaultContent: '',
                        visible: _permissions.edit,
                        rowAction: {
                            element: $("<button/>")
                                .addClass("btn btn-primary")
                                .text(app.localize('Detail'))
                                .click(function () {
                                    window.location.href = "/Accounting/DynamicEntityProperty/" + $(this).data().entityFullName;
                                })
                        }
                    },
                    {
                        targets: 2,
                        data: "entityFullName"
                    }
                ]
            });
        }
        
        $('#CreateNewDynamicEntityProperty').click(function () {
            _selectAnEntity.open();
        });

        $('#btnDynamicEntityPropertyTab').on('shown.bs.tab', function (e) {
            initializeTable();
        });
    });
})();
