/* jshint asi: true, node:true */
function bottomUpCutRod(p, n) {
  var r = new Array(n + 1)
  r[0] = 0
  for (var j = 1; j <= n; j++) {
    var q = -1
    for (var i = 1; i <= j; i++) {
      q = Math.max(q, p[i] + r[j - i])
    }
    r[j] = q
  }
  return r
}
console.log(bottomUpCutRod([0, 1, 5, 8, 9, 10, 17, 17, 20, 24, 30], 10))


function opti(costFunc, maxSubSize) {
/// returns an array of optimised costs to a range of subproblems
/// [costFunc](y, x, A) should return the cost of the subproblem given:
///   subproblem size [y]
///   a choice of [x]
///   an array [A] of all the optimised costs for all the subproblems with size smaller than y
/// [maxSubSize] is the biggest subproblem to optimise
  var r = new Array(maxSubSize + 1)
  r[0] = costFunc(0)
  for (var j = 1; j <= maxSubSize; j++) {
    var q = -1
    for (var i = 0; i <= j; i++) {
      q = Math.max(q, costFunc(j, i, r))
    }
    r[j] = q
  }
  return r
}

function optiCutRod(p, n) {
  return opti(function(j, i, r) {
    if (j === 0) {
      return 0
    }
    if (i === 0) { // cutting of 0 is an illegal choice
      return -1
    }
    return p[i] + r[j - i]
  }, n)
}

console.log(optiCutRod([0, 1, 5, 8, 9, 10, 17, 17, 20, 24, 30], 10))

function OptiLIS(A) {
  var n = A.length - 1
  return opti(function(y, x, opt) {
    if (y === 0) {
      return 1
    }
    if (A[x] < A[y]) {
      return opt[x] + 1
    } else {
      return 1
    }
  }, n)
}

console.log(OptiLIS([3, 5, -6, -1, 12, -6, -9, 3, 6, 1, -2, 5, -2, 9, -1]))


function LIS(A) {
  var n = A.length
  var lengths = new Array(n - 1)
  var indices = new Array(n - 1)
  var i
  for (i = 0; i <= n - 1; i++) {
    lengths[i] = 1
    indices[i] = null
  }
  for (i = 1; i <= n - 1; i++) {
    for (var j = 0; j <= i - 1; j++) {
      if (A[j] < A[i]) {
        var newBest = lengths[j] + 1
        if (newBest >= lengths[i]) {
          indices[i] = j
          lengths[i] = newBest
        }
      }
    }
  }

  var bestEndLen = -1
  var bestEndIndex = -1
  // find best length and index
  for (i = 0; i <= n - 1; i++) {
    if (bestEndLen < lengths[i]) {
      bestEndLen = lengths[i]
      bestEndIndex = i
    }
  }

  var arrayLength = bestEndLen - 1
  var output = new Array(arrayLength)

  var k = bestEndIndex
  for (i = bestEndLen - 1; i >= 0; i--) {
    output[i] = A[k]
    k = indices[k]
  }
  return lengths //output
}

console.log(LIS([3, 5, -6, -1, 12, -6, -9, 3, 6, 1, -2, 5, -2, 9, -1]))
