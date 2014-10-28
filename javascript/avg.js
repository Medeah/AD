function avgHelp (I, low, high) {

	if (low - high == 0) {
	    return I[low]
	}
	var mid = Math.floor((low+high)/ 2)
	var left = avgHelp(I, low, mid)
	var right = avgHelp(I, mid + 1, high)



	var out =  ((mid - low + 1)/(high-low + 1))*left + ((high - (mid +1) + 1)/(high-low + 1))*right

	return out
}

function avg (I) {
	return avgHelp(I, 0, I.length - 1)
}

//console.log(avg([5]))
console.log(avg([2,10]))
console.log(avg([17,30,70,1,87,20,10,74,36,80,43,23,16,46,53,88,95,62,100]))