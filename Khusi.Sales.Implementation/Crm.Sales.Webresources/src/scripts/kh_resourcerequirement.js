function onLoad(executionContext) {
    var formContext = executionContext.getFormContext();
    fixedCostLogic(formContext);
}

function onFixedCostChange(executionContext) {
    var formContext = executionContext.getFormContext();
    fixedCostLogic(formContext);
}

function fixedCostLogic(formContext) {
    var isFixedCost = formContext.getAttribute("kh_isfixedcost").getValue();
    if (isFixedCost) {
        alert("It is fixed cost");
        //kh_clienthourlyrate  kh_resourcehourlyrate
        //kh_clientfixedcostbudget kh_resourcefixedcost
        formContext.getAttribute("kh_clienthourlyrate").setValue(null);
        formContext.getAttribute("kh_resourcehourlyrate").setValue(null);
        formContext.getControl("kh_clienthourlyrate").setDisabled(true);
        formContext.getControl("kh_resourcehourlyrate").setDisabled(true);
        formContext.getControl("kh_clientfixedcostbudget").setDisabled(false);
        formContext.getControl("kh_resourcefixedcost").setDisabled(false);
    } else {
        alert("It is not fixed cost");
    }
}