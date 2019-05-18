var base_url = "http://localhost:63063/"

var AjaxHelper = function () {
	function getWithoutData(url, successCallback, failureCallBack) {
		$.ajax({
			url: base_url + url,
			dataType: "html",
			success: successCallback,
			error: failureCallBack
		});
	}

	function get(url, postData, successCallback, failureCallBack, context, asyncCall) {
		$.ajax({
			url: url,
			dataType: "json",
			type: "GET",
			contentType: 'application/json; charset=utf-8',
			async: asyncCall === undefined ? true : asyncCall,
			cache: false,
			data: postData,
			success: successCallback,
			error: failureCallBack,
			context: context,
			xhrFields: {
				withCredentials: true
			},
		});
	}

	function getWithBase(url, postData, successCallback, failureCallBack, context, asyncCall) {
		$.ajax({
			url: base_url + url,
			dataType: "json",
			type: "GET",
			contentType: 'application/json; charset=utf-8',
			async: asyncCall === undefined ? true : asyncCall,
			cache: false,
			data: postData,
			success: successCallback,
			error: failureCallBack,
			context: context
		});
	}

	function getView(url, postData, successCallback, failureCallBack, context, asyncCall) {
		$.ajax({
			context: context,
			url: base_url + url,
			dataType: "html",
			type: "GET",
			async: asyncCall === undefined ? true : asyncCall,
			cache: false,
			data: postData,
			success: successCallback,
			error: failureCallBack
		});
	}

	function getViewWithoutData(url, successCallback, failureCallBack, context, asyncCall) {
		$.ajax({
			context: context,
			url: base_url + url,
			dataType: "html",
			type: "GET",
			async: asyncCall === undefined ? true : asyncCall,
			cache: false,
			success: successCallback,
			error: failureCallBack
		});
	}

	function post(url, postData, successCallback, failureCallBack, context) {
		var options = {
			context: context,
			type: "POST",
			url: base_url + url,
			data: JSON.stringify(postData),
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: successCallback,
			failure: failureCallBack
		};
		$.ajax(options);
	}

	function postForView(url, postData, successCallback, failureCallBack, context) {
		var options = {
			context: context,
			url: base_url + url,
			dataType: "html",
			type: "POST",
			data: JSON.stringify(postData),
			contentType: "application/json; charset=utf-8",
			success: successCallback,
			failure: failureCallBack
		};
		$.ajax(options);
	}

	function postWithoutData(url, successCallback, failureCallBack) {
		$.ajax({
			type: "POST",
			url: base_url + url,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: successCallback,
			failure: failureCallBack
		});
	}

	function postFile(url, formData, successCallback, failureCallBack) {
		$.ajax({
			type: "POST",
			url: base_url + url,
			data: formData,
			dataType: "json",
			contentType: false,
			processData: false,
			success: successCallback,
			failure: failureCallBack
		});
	}

	return {
		getView: getView,
		get: get,
		getWithoutData: getWithoutData,
		getViewWithoutData: getViewWithoutData,
		post: post,
		postForView: postForView,
		postWithoutData: postWithoutData,
		postFile: postFile,
		getWithBase: getWithBase
	};
}();
