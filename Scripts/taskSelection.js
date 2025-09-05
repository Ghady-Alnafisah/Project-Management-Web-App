
function highlightSelected(btn) {
    // Clear previous selections
    document.querySelectorAll('.selected-task').forEach(el => {
        el.classList.remove('selected-task', 'table-primary');
    });
    // Highlight selected row
    const row = btn.closest('tr');
    row.classList.add('selected-task', 'table-primary');
    // Store TaskID in a hidden field
    document.getElementById('<%= hdnSelectedTask.ClientID %>').value = 
        btn.getAttribute('data-taskid');
}
    