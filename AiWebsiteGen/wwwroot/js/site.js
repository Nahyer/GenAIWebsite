const fade = document.getElementsByClassName("sec_hover");
for (let i = 0; i < fade.length; i++) {
  fade[i].addEventListener("mouseenter", (e) => {
    console.log(e);
    document.getElementById(e.target.id).style =
      "visibility:visible;opacity:0;transition:opacity 2s linear;";
  });
}
const hover_effect = document.getElementsByClassName("hover_effect");
const hide_el = document.getElementsByClassName("more");
const more_button = document.getElementsByClassName("more_btn");
const hide_card = document.getElementsByClassName("close");
let index;
for (i in hover_effect) {
  if (i < hover_effect.length) {
    hover_effect[i].addEventListener("mousemove", (e) => {
      document.getElementById(e.target.id).style.display = "block";
    });
    hide_el[i].addEventListener("mouseleave", (e) => {
      document.getElementById(e.target.id).style.display = "none";
    });
    more_button[i].addEventListener("click", (e) => {
      index = e.target.id.replace("btn", "det");
      document.getElementById(index).style.display = "flex";
      const j = parseInt(index.replace("det", ""));
      for (let p = 1; p <= hover_effect.length; p++) {
        if (p != j) {
          let gp = "det" + p;
          document.getElementById(gp).style.display = "none";
        }
      }
    });
    hide_card[i].addEventListener("click", (e) => {
      document.getElementById(index).style.display = "none";
    });
  }
}

const logo = document.getElementsByClassName("content");
const bg_loop = document.getElementsByClassName("rpt");
const repeat_inner = () => {
  for (let i = 0; i < logo.length; i++) {
    let p = logo[i].src;
    bg_loop[
      i
    ].style = `background-position:right;height:30vw;background-image:url('${p}');background-repeat:repeat;`;
    logo[i].style.display = "none";
  }
};
if (window.innerWidth <= 768) {
  repeat_inner();
}
window.addEventListener("resize", (e) => {
  if (window.innerWidth <= 768) {
    repeat_inner();
  } else {
    for (let i = 0; i < logo.length; i++) {
      logo[i].style.display = "block";
      bg_loop[i].style.display = "none";
    }
  }
});
const show = document.getElementsByClassName("container");

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
    top + height <= window.pageYOffset + window.innerHeight &&
    left + width <= window.pageXOffset + window.innerWidth
  );
}
window.addEventListener("scroll", (e) => {
  console.log(e);
  for (let i = 0; i < show.length; i++) {
    if (elementInViewport(show[i])) {
      show[i].style["z-index"] = "1";
    } else {
      show[i].style["z-index"] = "-1";
    }
  }
});
