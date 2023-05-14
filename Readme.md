<h1>Points to remember</h1>

<ul>
<li> Hard coded string will not be passed as parameters in the query sent to the database. So create variables and pass them
to the predicate.</li>
<li> Find will translate to select TOP(1) </li>
<li> If we have multiple orderby's then LINQ qill ignore all but the last one so use ThenBy if we need multiple orderby's </li>
<li> DBContext.Add/Update will set the entity state to added or modified and then the context will persist these changes when
savechanges is called. In case of disconnected scenarios we need to explicity use these commands to set the entity state. </li>
<li>In connected scenarios we can directly call the savechanges method as the context will be tracking the changes and will
set the entity state for us.</li>
<li> use the DebugView on the ChangeTracker property on the context to find out how EF is tracking the entities.</li>
<li> DBSet.Add will set the entity state to added and Update will set the entity state to modified. So be carefull when updating data
as it will generate an insert statement in sql if we try to update an exisiting entity and add use the add method. </li>
<li> Where and OrderBy inside the include statement will be added to the sql query sent to the database</li>
<li> By default delete on cascade is turned on. SO inorder to not delete the related entities just set the foreign key to null</li>
</ul>