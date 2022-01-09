function onLoad(executionContext) {
    var formContext = executionContext.getFormContext();
    
}

function onContactTypeChange(executionContext) {
    var formContext = executionContext.getFormContext();
    var projectStartDate = formContext.getAttribute("wh_startdate").getValue();
    if (projectStartDate != null) {
        let projectYear = projectStartDate.getFullYear();
        formContext.getAttribute("wh_projectyear").setValue(projectYear);
        formContext.getControl("wh_projectyear").setDisabled(true);
    }
}