function hide(obj) {	
	
	if(obj.parentElement.firstElementChild.innerHTML == '<span class="glyphicon glyphicon-menu-right" aria-hidden="true"></span>'){
		obj.parentElement.firstElementChild.innerHTML = '<span class="glyphicon glyphicon-menu-down" aria-hidden="true"></span>';
	}
	else {
		obj.parentElement.firstElementChild.innerHTML = '<span class="glyphicon glyphicon-menu-right" aria-hidden="true"></span>';
	}
}