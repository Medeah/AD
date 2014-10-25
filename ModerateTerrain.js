var assert = require('assert');
a = [[2,3,5,6],
      [1,2,5,5],
      [4,4,3,3],
      [4,4,9,1]]

b = [[2,2,6,7],
      [4,4,9,6],
      [4,9,7,3],
      [8,3,3,3]]



assert(ModerateTerrain([[1]]))
assert(ModerateTerrain([[0]]))
assert(ModerateTerrain([[2,2],[2,2]]))
assert(!ModerateTerrain([[3,3],[9,1]]))
assert(!ModerateTerrain(a))
assert(ModerateTerrain(b))

function ModerateTerrain (A) {
    return modHelp(A, 0, A.length - 1, 0, A.length - 1)[0]
}

function modHelp (A, fx, tx, fy, ty) {
    var width = tx - fx + 1

    if(width === 1) {
        return [true, A[fy][fx]]
    }

    var midx = tx - width/2
    var midy = ty - width/2

    var one = modHelp(A, fx, midx, fy, midy)
    var two = modHelp(A, midx + 1, tx, fy, midy)
    var thr = modHelp(A, fx, midx, midy + 1, ty)
    var fou = modHelp(A, midx + 1, tx, midy + 1, ty)

    var avg = (one[1] + two[1] + thr[1] + fou[1]) / 4

    return [one[0] && two[0] && thr[0] && fou[0] &&
        avg * 2 >= one[1] && one[1] >= avg / 2 &&
        avg * 2 >= two[1] && one[1] >= avg / 2 &&
        avg * 2 >= thr[1] && one[1] >= avg / 2 &&
        avg * 2 >= fou[1] && one[1] >= avg / 2, avg]
}