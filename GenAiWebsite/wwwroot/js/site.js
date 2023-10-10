const c = document.getElementsByClassName("hover_effect");
const g = document.getElementsByClassName("more");
const b = document.getElementsByClassName("more_btn");
const close = document.getElementsByClassName("close");
const r = document.getElementsByClassName("content");
const rpt = document.getElementsByClassName("rpt");
let idx;

const fn = () => {
    for (let i = 0; i < r.length; i++) {
        let p = r[i].src;
        console.log(p);
        rpt[i].style = `background-position:right;height:30vw;background-image:url('${p}')`;
        rpt[i].style.backgroundRepeat = "repeat";
        console.log(rpt);
        r[i].style.display = "none";
    }
}
if (window.innerWidth <= 768) {
    fn();
}
window.addEventListener("resize", (e) => {
    if (window.innerWidth <= 768) {
        fn();
    } else {
        for (let i = 0; i < r.length; i++) {
            r[i].style.display = "block";
            rpt[i].style.display = "none";
        }
    }
})
const y = document.getElementsByClassName("container");
function elementInViewport(el) {
    var top = el.offsetTop;
    var left = el.offsetLeft;
    var width = el.offsetWidth;
    var height = el.offsetHeight;

    while (el.offsetParent) {
        el = el.offsetParent;
        top += el.offsetTop;
        left += el.offsetLeft;
    }

    return (
        top >= window.pageYOffset &&
        left >= window.pageXOffset &&
        (top + height) <= (window.pageYOffset + window.innerHeight) &&
        (left + width) <= (window.pageXOffset + window.innerWidth)
    );
}
window.addEventListener("scroll", (e) => {
    for (let i = 0; i < y.length; i++) {
        if (elementInViewport(y[i])) {
            y[i].style["z-index"] = "1";
        } else {
            y[i].style["z-index"] = "-1";
        }
    }
})
for (i in c) {
    c[i].addEventListener("mousemove", (e) => {
        document.getElementById(e.target.id).style.display = "block";
    })
    g[i].addEventListener("mouseleave", (e) => {
        document.getElementById(e.target.id).style.display = "none";
    })
    b[i].addEventListener("click", (e) => {
        idx = e.target.id.replace("btn", "det")
        document.getElementById(idx).style.display = "flex"
        const j = parseInt(idx.replace("det", ""));
        for (let p = 1; p <= 4; p++) {
            if (p != j) {
                let gp = "det" + p;
                document.getElementById(gp).style.display = "none"
            }
        }
    })
    close[i].addEventListener("click", (e) => {
        document.getElementById(idx).style.display = "none"
    })
}
//    // Function to toggle the visibility of div1
//     document.querySelector("#tools").addEventListener("click", function () {
//       var div1 = document.querySelector("#div1");
//       if (div1.style.display === "none" || div1.style.display === "") {
//         div1.style.display = "block";
//         document.querySelector("#div2").style.display = "none"; // Hide div2 if it's open
//       } else {
//         div1.style.display = "none";
//       }
//     });

//     document.querySelector('#org').addEventListener('click', function() {
//         var div2 = document.querySelector('#div2');
//         if (div2.style.display === 'none' || div2.style.display === '') {
//             div2.style.display = 'block';
//             document.querySelector('#div1').style.display = 'none'; 
//         } else {
//             div2.style.display = 'none';
//         }
//     });