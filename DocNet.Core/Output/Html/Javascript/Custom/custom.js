function hide(obj, num) {	
	useID = '#collapse' + num;
	
	if(obj.innerHTML == '<span class="glyphicon glyphicon-menu-right" aria-hidden="true"></span>'){
		obj.innerHTML = '<span class="glyphicon glyphicon-menu-down" aria-hidden="true"></span>';
		$(useID).collapse('toggle');
	}
	
	else {
		obj.innerHTML = '<span class="glyphicon glyphicon-menu-right" aria-hidden="true"></span>';
		$(useID).collapse('toggle');
	}
}