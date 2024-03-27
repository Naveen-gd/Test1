import os, sys, shutil

ignore = ['_unused', '__pycache__']

if len(sys.argv) == 2:
    src = sys.argv[0]
    dst = sys.argv[1]
else:
    src = '../../elmos-521_38-guard-ic-fw/test'
    dst = './bin/Debug/Lib'

def copy_tree(src, dst):
    for root, dirs, files in os.walk(src):
        root = root.replace('\\', '/')
        if root.split('/')[-1] in ignore:
            continue
        for file in files:
            if not os.access(dst + root[len(src):], os.F_OK):
                os.mkdir(dst + root[len(src):])
            print('copy', root + '/' + file, dst + root[len(src):])
            shutil.copy(root + '/' + file, dst + root[len(src):])
            
            
copy_tree(src, dst)

src = './Lib'
dst = './bin/Debug/Lib'
copy_tree(src, dst)